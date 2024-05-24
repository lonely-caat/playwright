import { v4 as uuidv4 } from 'uuid';
import { faker } from '@faker-js/faker';


interface PayloadOptions {
    textValue: string;
    user?: string;
    fileId?: string;
}

export function generatePayload(options: PayloadOptions) {
    const now = new Date();
    const createdDate = new Date(now);
    createdDate.setDate(createdDate.getDate() - 1);
    const eventTimeDate = new Date(createdDate);
    eventTimeDate.setHours(eventTimeDate.getHours() + 1);
    const lastModificationTimeDate = new Date(now);
    lastModificationTimeDate.setHours(lastModificationTimeDate.getHours() - 1);

    function formatDate(date: Date): string {
        return date.toISOString().slice(0, -1) + 'Z';
    }

    return {
        created: formatDate(createdDate),
        eventTime: formatDate(eventTimeDate),
        user: options.user || faker.person.lastName().replace('\'', ''),
        agentId: "system/n2n",
        ipAddress: "127.0.0.1",
        fileTextEvent: {
            classifiedFile: {
                fileId: options.fileId || uuidv4(),
                path: "/path",
                mimeType: "txt",
                contentLength: "1",
                lastModificationTime: formatDate(lastModificationTimeDate)
            },
            settings: {
                promptRequested: true
            },
            text: options.textValue
        }
    };
}
