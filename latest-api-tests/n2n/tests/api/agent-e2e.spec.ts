import { test, expect } from '@playwright/test';
import { sendFileTextEvent } from '../../logic/sendFileGRPC';
import { generateLocalizedText } from '../../logic/generateLocalizedText';
import { Language } from '../../types/languageTypes';

const wordCounts = [0, 1, 50, 500, 10000];
test.describe.parallel('gRPC FileTextEvent endpoint validation with varying word counts', () => {
  for (let count of wordCounts) {
    test(`should handle a file with ${count} words`, async () => {
      try {
        const localizedText = await generateLocalizedText('English', count);
        const response = await sendFileTextEvent(localizedText);
        expect(response).not.toEqual({});
      } catch (error) {
        console.error(error);
        throw error;
      }
    });
  }
});

const languageList: Language[] = [
  'Arabic',
  'German',
  'Spanish',
  'French',
  'Hebrew',
  'Italian',
  'Polish',
  'Portuguese',
  'Thai',
  'Chinese',
  'Special',
];
test.describe.parallel('gRPC FileTextEvent endpoint validation with text files in various languages', () => {
  for (let language of languageList) {
    test(`should process file text in ${language} correctly`, async () => {
      try {
        const localizedText = await generateLocalizedText(language, 100);
        const response = await sendFileTextEvent(localizedText);
        expect(response).not.toEqual({});
      } catch (error) {
        console.error(error);
        throw error;
      }
    });
  }
});
