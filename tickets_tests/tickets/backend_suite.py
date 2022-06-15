import os
import configparser
import requests
import json
from jsonschema import validate

class BackendSuite(object):
    def __init__(self):
        self.conf = self.read_config()
        self.url = self.conf['backend']['host']

    @staticmethod
    def read_config():
        __location__ = os.path.realpath(
            os.path.join(os.getcwd(), os.path.dirname(__file__)))
        config_file = __location__ + '/' + 'config/config.ini'
        cp = configparser.ConfigParser()
        cp.read(config_file)
        return cp

    # TODO: break this into multiple functions
    def make_request(self, path, method='GET', parameters=None, body=None):
        params = {}
        data = {}

        if parameters:
            params.update(parameters)
        if body:
            data.update(body)

        url = self.url + '/' + path

        if method.upper() == 'POST':
            response = requests.post(url=url, params=params, headers={"Content-Type": "application/json"}, data=json.dumps(data))
        elif method.upper() == 'PATCH':
            response = requests.patch(url=url, params=params, headers={"Content-Type": "application/json"}, data=json.dumps(data))
        else:
            response = requests.get(url=url, params=params)

        try:
            return{"code": response.status_code, "body": response.json()}
        except ValueError:
            return ["an error occurred! status code:", response.status_code, "but the response is: ", response.text]

    @staticmethod
    def validate_agent_response(response):
        schema = {
            "type": "object",
            "properties": {
                "id": {"type": "string"},
                "name": {"type": "string"},
                "status": {"type": "string"}
            }}

        return validate(instance=response, schema=schema)

    @staticmethod
    def validate_issues_response(response):
        schema = {
            "type": "object",
            "properties": {
                "agent_id": {"type": "string"},
                "status": {"type": "string"},
                "title": {"type": "string"}
            }}

        return validate(instance=response, schema=schema)

    # TODO rewrite this into DB query
    def create_agent(self, name):
        requests.post(url=self.url+'/agents', data=json.dumps({'name': name}))

    # TODO rewrite this into DB query
    def return_agent_id(self, name):
        """
        :param name: string, agent name
        Returns a list of ids associated with agent name provided
        """
        self.create_agent(name)
        response = requests.get(url=self.url + '/agents')
        return [x['id'] for x in response.json() if x['name'] == 's']

    # TODO rewrite this into DB query
    def create_issue(self, title, status='new'):
        requests.post(url=self.url + '/issues', data=json.dumps({'title': title, 'status:': status}))



