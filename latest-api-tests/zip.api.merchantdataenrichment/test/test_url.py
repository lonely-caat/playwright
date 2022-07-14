#!/usr/bin/env python3
import os
import pytest
import requests


def test_url():
    r = requests.get(os.environ['TEST_URL'], verify=False)
    assert(r.status_code == 200)
    assert('Healthy' in r.text)