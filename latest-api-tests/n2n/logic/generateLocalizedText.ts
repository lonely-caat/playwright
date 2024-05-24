import { promises as fs } from 'fs';
import { join } from 'path';
import { Language } from '../types/languageTypes';

const LANGUAGE_FILE_MAP: Record<Language, string> = {
  Arabic: 'arabic.txt',
  German: 'german.txt',
  Spanish: 'spanish.txt',
  French: 'french.txt',
  Hebrew: 'hebrew.txt',
  Italian: 'italian.txt',
  Polish: 'polish.txt',
  Portuguese: 'portuguese.txt',
  Thai: 'thai.txt',
  Chinese: 'chinese.txt',
  English: 'english.txt',
  Special: 'special.txt',
};

async function generateLocalizedText(
  language: Language,
  length: number,
): Promise<string> {
  const filePath = join(
    __dirname,
    '../payloads/languageFiles',
    LANGUAGE_FILE_MAP[language],
  );
  const fileContent = await fs.readFile(filePath, 'utf-8');

  const words = fileContent.split(/\s+/);
  const selectedWords = [];

  for (let i = 0; i < length; i++) {
    selectedWords.push(words[i % words.length]);
  }

  return selectedWords.join(' ');
}

export { generateLocalizedText };
