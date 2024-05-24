import { v4 as uuidv4 } from 'uuid';
import {connectorSecrets} from "../playwright.config";



export function createAwsConnector(alias:string) {
    return {
        "alias":alias,
        "connectionDetails":{
            "accessKey": connectorSecrets.awsS3.connectionDetails.accessKey,
            "secretAccessKey": connectorSecrets.awsS3.connectionDetails.secretAccessKey,
            "containerPath":"",
            "displayPath":""
        }
    }
}

export function createAzureConnector(alias:string) {
    return {
        "alias":alias,
        "connectionDetails":{
            "ConnectionString": connectorSecrets.azureBlobFiles.connectionString,
            "containerPath":"",
            "displayPath":""
        }
    }
}

export function createConfluenceCloudConnector(alias:string) {
    return {
        "alias":alias,
        "connectionDetails":{
            "username": connectorSecrets.confluenceCloud.connectionDetails.username,
            "apiToken": connectorSecrets.confluenceCloud.connectionDetails.apiToken,
            "domain": connectorSecrets.confluenceCloud.connectionDetails.domain,
            "containerPath":"",
            "displayPath":""
        }
    }
}

export function createGoogleDriveConnector(alias:string) {
    return {
        "alias":alias,
        "clientEmail": connectorSecrets.googleDrive.clientEmail,
        "privateKey": connectorSecrets.googleDrive.privateKey,
        "domain": connectorSecrets.googleDrive.domain,
        "userId": connectorSecrets.googleDrive.userId,
        "selectedUserId": "",
        "scanType": "files",
        "folderId": "",
        "containerPath":"",
        "displayPath":""
    }
}

export function createLDAPConnector(alias:string) {
    return {
        "alias": alias,
        "administratorName": connectorSecrets.ldap.administratorName,
        "administratorPassword": connectorSecrets.ldap.administratorPassword,
        "host": connectorSecrets.ldap.host,
        "certificate": "",
        "port": 389,
        "inactivePeriod": 90,
        "searchBase":"DC=aws-gv,DC=local"
    }
}

export function createsmbConnector(alias:string) {
    return {
        "data": {
            "alias": alias,
            "userName": connectorSecrets.cifs.userName,
            "password": connectorSecrets.cifs.password,
            "domain": connectorSecrets.cifs.domain,
            "host": connectorSecrets.cifs.host,
            "port": connectorSecrets.cifs.port,
            "path": "/thai-regex/"
        }
    }
}

export function createOneDriveConnector(alias:string) {
    return {
        "alias":alias,
        "adminUserId":connectorSecrets.oneDrive.adminUserId,
        "tenantId":connectorSecrets.oneDrive.tenantId,
        "clientId":connectorSecrets.oneDrive.clientId,
        "clientSecret":connectorSecrets.oneDrive.clientSecret,
        "folderId":"",
        "userId":"",
        "path":"",
        "scanType":"files"
    }
}

export function createSharepointOnLineConnector(alias:string, path:string="") {
    return {
        "alias":alias,
        "ownerID":null,
        "locationIsoA3":[],
        "connectionDetails":{

        "displayPath":path,
        "containerPath":"",
        "tenantId":connectorSecrets.sharepointOnline.tenantId,
        "clientId":connectorSecrets.sharepointOnline.clientId,
        "clientSecret":connectorSecrets.sharepointOnline.clientSecret,
    }
    }
}

export function createSharepointOnPremConnector(alias:string) {
    return {
        "alias":alias,
        "username":connectorSecrets.sharepointOnPremise.username,
        "password":connectorSecrets.sharepointOnPremise.password,
        "domain":connectorSecrets.sharepointOnPremise.domain,
        "scanType":"files",
        "siteUrl":"",
        "path":""
    }
}

export function createBoxConnector(alias:string) {
    return {
        "alias":alias,
        "connectionDetails":{
            "containerPath":"",
            "userID":connectorSecrets.box.userID,
            "enterpriseID":connectorSecrets.box.enterpriseID,
            "clientID":connectorSecrets.box.clientID,
            "clientSecret":connectorSecrets.box.clientSecret,
            "publicKeyID":connectorSecrets.box.publicKeyID,
            "privateKey":connectorSecrets.box.privateKey,
            "passphrase":connectorSecrets.box.passphrase,
            "displayPath":""
        }
    }
}
