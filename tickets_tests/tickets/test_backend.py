from .backend_suite import BackendSuite
import pytest


class TestTickets(object):
    client = BackendSuite()

    def test_get_agents(self):
        """Test Case - Backend - agents.
            Pre-Conditions:
            ---------------
            API is up and running on host specified in config.ini file
            make sure there are agents pre-created in the DB

            Steps:
            ------
            0. Create an agent
            1. Send GET /agents request to get list of agents
            -----------------
            200 status code received
            Data schema is valid
        """
        TestTickets.client.create_agent(name='test')
        response = TestTickets.client.make_request('agents')

        assert response['code'] == 200, "Expected Status 200"
        for agent in response['body']:
            TestTickets.client.validate_agent_response(agent)

    @pytest.mark.parametrize("agent_name, expected_status", [("s", 201), ("1", 201), ("Kätzchen mögen Spaß", 201),
                                                             ("wee"*1000, 400), ("", 400), (" ", 400)])
    def test_create_agents(self, agent_name, expected_status):
        """Test Case - Backend - agents.
            Pre-Conditions:
            ---------------
            API is up and running on host specified in config.ini file

            Steps:
            ------
            1. Send POST /agents request to create a new agent
            -----------------
            200 status code received
        """
        response = TestTickets.client.make_request('agents', method='POST', body={"name": agent_name})
        assert response['code'] == expected_status, f"Expected Status ${expected_status}"

    def test_get_agents_tickets(self):
        """Test Case - Backend - agents.
            Pre-Conditions:
            ---------------
            API is up and running on host specified in config.ini file

            Steps:
            ------
            1. Send GET /agents/id/issues request to get list of issues assigned to an agent
            -----------------
            200 status code received
            Data schema is valid
        """
        ids = TestTickets.client.return_agent_id('test')
        response = TestTickets.client.make_request(f'/agents/${ids[0]}/issues')
        assert response['code'] == 200, "Expected Status 200"
        assert 'SELECT' not in response['body'], "Did not expect to receive a DB query in response"

    def test_get_issues(self):
        """Test Case - Backend - issues.
            Pre-Conditions:
            ---------------
            API is up and running on host specified in config.ini file
            make sure there are issues pre-created in the DB

            Steps:
            ------
            0. Create an issue
            1. Send GET /issues request to get list of issues
            -----------------
            200 status code received
            Data schema is valid
        """
        TestTickets.client.create_issue(title='test')
        response = TestTickets.client.make_request('issue')
        for issue in response['body']:
            TestTickets.client.validate_agent_response(issue)
        assert response['code'] == 200, "Expected Status 200"

    @pytest.mark.parametrize("title, status, expected_status_code", [("Bug", "new",  201), ("1", "0", 400),
    ("wee"*1000, "new", 400), ("valid", "invalid"*1000, 400), ("Kätzchen mögen Spaß", "new", 400), ("Valid", "мяу",400),
    ("", "new", 400), ("Valid", "", 400), (" ", "'", 400), ("±!@#$%^&*()_+", "<>?~}|{:", 400)])
    def test_create_issues(self, title, status, expected_status_code):
        """Test Case - Backend - issues.
            Pre-Conditions:
            ---------------
            API is up and running on host specified in config.ini file

            Steps:
            ------
            1. Send POST /issues request to create a new agent
            -----------------
            200 status code received
        """
        response = TestTickets.client.make_request('issue', method='POST', body={"title": title,
                                                                                  "status": status})
        assert response['code'] == expected_status_code, f"Expected Status ${expected_status_code}"
