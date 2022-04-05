import dotenv from 'dotenv-safe';

dotenv.config();

export const baseURL = process.env.BASE_URL;

export const product = {
  ZipPay: 'ZipPay',
  ZipCredit: 'ZipMoney',
};

export const price = '10';

export const apiEndPoints = {
  createInstorePin: 'createPin',
};

export const orderStatus = {
  Status: 'status',
  Completed: 'Completed',
  Authorised: 'Authorised',
  Refunded: 'Refunded',
};

export const applicationStatus = {
  Status: 'status',
  Registered: 'Registered',
  Approved: 'Approved',
  Declined: 'Declined',
};

export const userManagement = {
  firstName: 'AutomationUser',
  lastName: 'Userrole',
  updateLastName: 'updated',
};

export const currentCountry = 'AU';

export const customerEmail = {
  zipPay: {
    email: 'partner-automation-customer@mailinator.com',
  },
  zipCredit: {
    email: 'zm-automation-customer@mailinator.com',
    mobileNumber: '0400 000 222',
  },
};

export const genericContactNo = '0400000000';

export const sendInviteCustomer = {
  firstName: 'partnerorigination',
  lastName: 'APPROVETEST',
};

export const partnerDashboard = {
  merchant1: {
    merchantName: 'DashboardAutomationMerchant1',
    adminEmail: 'automation-user-donotuse@mailinator.com',
  },
  merchant2: {
    merchantName: 'DashboardAutomationMerchant2',
    adminEmail: 'dashboard-automation-user@mailinator.com',
  },
  company: {
    adminEmail: 'automation-company-user-donotuse@mailinator.com',
  },
  user: {
    userName: 'merchant_user@zipteam222898.testinator.com'
  },
};

export const newDash = {
  merchant1: {
    merchantName: 'bas-testing1@mailinator.com',
    merchantPassword: 'Password1',
  },
  merchant2: {
    merchantName: 'bas-testing8@zipteam222898.testinator.com',
    merchantPassword: 'Password1'
  },
  merchant3: {
    merchantName: 'automation-user-donotuse@mailinator.com',
    merchantPassword: 'Test1234'
  },
  merchant4: {
    merchantName: 'bas-testing4@mailinator.com',
    merchantPassword: 'Password1'
  }
};


export const partnerReports = {
  merchant1: {
    merchantName: 'zipBill',
    adminEmail: 'bas-testing1@mailinator.com',
    merchantId: 2134,
    password: 'Password1',
  },
  merchant2: {
    merchantName: 'Marqeta Dev Test',
    adminEmail: 'bas-testing2@mailinator.com',
    merchantId: 34072,
    password: 'Password1',
  },
  merchant3: {
    merchantName: 'BAS Testing Company',
    adminEmail: 'bas-testing3@mailinator.com',
    companyId: 10037,
    merchantId: [2007, 2134],
    password: 'Password1',

  },
};

export const orderEmail = 'order@zipteam222898.testinator.com'

export const partnerPassword = 'Test1234';

export const defaultPassword = 'test1234';

export const defaultSmsCode = '123456';

export const partnerDetails = {
  abn: '15000072257',
  firstName: 'MerchantTest',
  surName: 'Automation',
  midName: 'Meow',
  directorFullname: 'QAAutomation',
  licenceNumber: '1111111111',
  dob: '12121980',
  bsbNo: '123456',
  accountNumber: '1234567',
};

export const partnerReference = 'meow'

export const zipUtils = {
  createUser: {
    baseUrl: 'http://qa-utils-1866972975.ap-southeast-2.elb.amazonaws.com',
    endpoint: '/createuser',
    token: 'AutomationTest',
  },
};


