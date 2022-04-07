#!/usr/bin/env python3
import os
import pytest
import requests
import warnings
from requests.packages.urllib3.exceptions import InsecureRequestWarning
from pprint import pprint

def test_url():
    warnings.simplefilter('ignore',InsecureRequestWarning)
    r = requests.get(os.environ['TEST_URL'], verify=False)
    pprint(os.environ['TEST_URL'])
    pprint(r.text)
    assert(r.status_code == 200)
