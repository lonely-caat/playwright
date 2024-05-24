import { exec } from 'child_process';
import { promisify } from 'util';
import { generatePayload } from '../payloads/onFileTextEventGRPC.payload';
import { defaultBaseURL } from '../playwright.config';

const execAsync = promisify(exec);

export async function sendFileTextEvent(textValue: string): Promise<string> {
  const payload = generatePayload({ textValue });
  const host = defaultBaseURL.replace(/https?:\/\//, '');

  const command = `
        grpcurl -insecure \
        -H "Authorization: Bearer ${process.env.API_TOKEN}" \
        -d '${JSON.stringify(payload)}' \
        ${host}:443 \
        com.getvisibility.grpc.agentedge.FileTextListener.OnFileTextEvent
    `;

  const { stdout, stderr } = await execAsync(command);

  // Check for seen error patterns
  if (stdout.includes('ERROR:')) {
    console.error(`gRPC Error in stdout: ${stdout}`);
    throw new Error(stdout);
  }

  if (stderr) {
    console.error(`gRPC Error in stderr: ${stderr}`);
    throw new Error(stderr);
  }

  return stdout;
}
