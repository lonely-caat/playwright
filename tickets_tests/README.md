Host configuration:
-------------------------
- Python 3.9.7
- Virtualenv properly set-up with the LATEST version of setuptools and pip.
You can find more info about python packages and virtualenv here:
https://packaging.python.org/guides/installing-using-pip-and-virtual-environments/
- Just clone and go into the project and run ```python3 setup.py install```


Run tests:
```
pytest /tmp/test/tickets/test_backend.py
```
Step-by-step installation on localhost:
```
$ cd /tmp
$ git clone https://github.com/meow-meow1/test.git
$ cd test
$ virtualenv --python=python3.9.7 venv/
$ source venv/bin/activate
(venv)$ pip3 install -U pip setuptools
(venv)$ python3 setup.py install
(venv)$ pytest tickets/test_backend.py
```
Run tests with report:
```
python3 -m pytest --html=./report.html /tmp/test/tickets/test_backend.py
```
