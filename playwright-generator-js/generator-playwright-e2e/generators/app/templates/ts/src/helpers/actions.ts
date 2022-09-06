/* eslint-disable @typescript-eslint/no-unused-expressions */
import { Frame, Response, Page } from 'playwright';
import { isHeadless } from '../config';

type WaitResponseOptions = {
  status?: number;
  method?: string;
  message?: string;
};

export default (p: Page, f: Frame | null = null) => {
  let document: any;
  const page = f || p;
  return {
    selectItemFromDropdown: async (item: string, dropdown: string) => {
      await page.click(dropdown);
      await page.waitForTimeout(150);
      await page.click(item);
      await page.waitForTimeout(400);
    },
    getPageHeaderText: async () => {
      await page.waitForNavigation({ waitUntil: 'domcontentloaded' });
      return page.innerText('h1');
    },
    switchToiFrameBySelector: async (selector: string) => {
      const containerFrame = await page.waitForSelector(selector);
      const containerContent = await containerFrame!.contentFrame();
      return containerContent!;
    },
    // https://github.com/microsoft/playwright/issues/3166
    clickFrameElement: async (selector: string) => {
      const options = { position: { x: 50, y: 85 }, force: true };
      isHeadless
        ? await page.$eval(selector, (element: any) => element.click())
        : await page.click(selector, options);
    },
    getPageDimensions: async () => page.evaluate(() => ({
      width: document.documentElement.clientWidth,
      height: document.documentElement.clientHeight,
    })),
    waitForResponse: async (url: string, opts: WaitResponseOptions): Promise<Response> => {
      try {
        const response = await p.waitForResponse((res: Response) => {
          const { status, method } = opts;
          const urlTest = res.url().includes(url);
          const statusTest = status ? res.status() === status : true;
          const methodTest = method ? res.headers().method === method : true;
          return urlTest && statusTest && methodTest;
        });
        return response;
      } catch (error) {
        const { message, status } = opts;
        throw Error(`Timeout waiting 1 minute for ${url}
                            ${status ? ` with status ${status}` : ''}
                            ${message ? ` ${message}` : ''}`);
      }
    },
  };
};
