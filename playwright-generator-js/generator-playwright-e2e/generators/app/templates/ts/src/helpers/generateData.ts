import { v4 } from 'uuid';

export const createRandomEmail = () => `qa-${v4()}@mailinator.com`;

export const randomId = () => v4().replace(/-/g, '');
