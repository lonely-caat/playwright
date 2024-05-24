import { APIRequestContext, expect } from '@playwright/test';
import {
  createAwsConnector,
  createAzureConnector,
  createBoxConnector,
  createGoogleDriveConnector,
  createLDAPConnector,
  createOneDriveConnector,
  createSharepointOnLineConnector,
  createSharepointOnPremConnector,
  createConfluenceCloudConnector,
  createsmbConnector,
} from '../../payloads/connectors';

interface ConnectorConfig {
  createConnector: (alias: string) => any;
  endpoint: string;
}

type ConnectorType =
  | 'box'
  | 'aws'
  | 'azureFiles'
  | 'googleDrive'
  | 'ldap'
  | 'oneDrive'
  | 'sharePointOnPremise'
  | 'sharePointOnLine'
  | 'confluenceCloud'
  | 'smb'
  | 'azureBlob';

const connectorConfig: Record<ConnectorType, ConnectorConfig> = {
  box: {
    createConnector: createBoxConnector,
    endpoint: '/scan-manager/connector/v2/configuration/box',
  },
  aws: {
    createConnector: createAwsConnector,
    endpoint: '/scan-manager/connector/v2/configuration/aws_s3',
  },
  azureFiles: {
    createConnector: createAzureConnector,
    endpoint: '/scan-manager/connector/v2/configuration/azure_files',
  },
  googleDrive: {
    createConnector: createGoogleDriveConnector,
    endpoint: '/scan-manager/connector/v2/configuration/google_drive',
  },
  ldap: {
    createConnector: createLDAPConnector,
    endpoint: '/scan-manager/connector/v2/configuration/ldap',
  },
  oneDrive: {
    createConnector: createOneDriveConnector,
    endpoint: '/scan-manager/connector/v2/configuration/onedrive',
  },
  sharePointOnLine: {
    createConnector: createSharepointOnLineConnector,
    endpoint: '/scan-manager/connector/v2/configuration/sharepoint_online',
  },
  sharePointOnPremise: {
    createConnector: createSharepointOnPremConnector,
    endpoint: '/scan-manager/connector/v2/configuration/sharepoint-on-prem',
  },
  confluenceCloud: {
    createConnector: createConfluenceCloudConnector,
    endpoint: '/scan-manager/connector/v2/configuration/confluence_cloud',
  },
  smb: {
    createConnector: createsmbConnector,
    endpoint: '/scan-manager/connections/smb',
  },
  azureBlob: {
    createConnector: createAzureConnector,
    endpoint: '/scan-manager/connector/v2/configuration/azure_blob',
  },
};

export async function createAndPostConnector(apiContext: APIRequestContext, connectorType: string, alias: string, path?: string) {
  const config = connectorConfig[connectorType];
  if (!config) {
    throw new Error(`Unsupported connector type: ${connectorType}`);
  }

  const connector = config.createConnector(alias, path);
  const postResponse = await apiContext.post(config.endpoint, {
    data: connector,
  });
  await expect(postResponse).toBeOK();
  const resp = await postResponse.json();
  return postResponse;
}

export async function deleteConnector(apiContext: APIRequestContext, connectorType: ConnectorType, alias: string) {
  const config = connectorConfig[connectorType];
  if (!config) {
    console.log(`Unsupported connector type: ${connectorType}`);
    return;
  }

  // Fetching all connectors of the specified type
  const response = await apiContext.get(config.endpoint + '?limit=1000');
  const data = await response.json();
  const items = data.items || [];

  // Finding the connector id by its alias
  const connector = items.find((item) => item.alias === alias)?.id
  if (!connector) {
    console.log(`No connector found with alias: ${alias}`);
    return;
  }

  // Deleting the connector
  const deleteResponse = await apiContext.delete(`${config.endpoint}/${connector}`);
  const responseBody = await deleteResponse.text();
  return responseBody;
}
