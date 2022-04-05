require('dotenv-safe').config();


const completeprofile = {
"header": "Great progress, Merchanttest!"
};
const businessdetails =  {
"header": "Business details"
};
const emailSubject = {
"invite": "Complete your DashboardAutomationMerchant1 application with Zip Pay",
"sendorder": "Complete your DashboardAutomationMerchant1 order with Zip Pay",
"setpassword": "Set your Zip dashboard password"
};
const userManagement = {
"addUser": "An email has been sent to the user. Please note, the password link expires in 7 days.",
"editUser": "User has been updated successfully.",
"deleteUser": "User has been deleted successfully."
};
const mailinator = {
"domain": "zipteam222898.testinator.com",
"baseURL": "https://mailinator.com/api/v2/domains/zipteam222898.testinator.com/inboxes",
"apiKey": "0472fb01852f40eab0e00d0f197eac6a"
};
const createUser = {
"baseUrl": "http://qa-utils-1866972975.ap-southeast-2.elb.amazonaws.com",
"endpoint": "/createuser",
"token": "AutomationTest"
};

const currentEnvironment = 'sandbox';

exports.completeprofile = completeprofile;
exports.businessdetails = businessdetails;
exports.emailSubject = emailSubject;
exports.currentEnvironment = currentEnvironment;
exports.userManagement = userManagement;
exports.createUser = createUser;
exports.mailinator = mailinator;