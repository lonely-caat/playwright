const { isHeadless } = require('../config');

const actions = (pageInstance, frameInstance = null) => {
  const page = frameInstance || pageInstance;
  return {
    selectItemFromDropdown: async (item, dropdown) => {
      await page.click(dropdown);
      await page.waitForTimeout(150);
      await page.click(item);
      await page.waitForTimeout(400);
    },
    getPageHeaderText: async () => {
      await page.waitForNavigation({ waitUntil: 'domcontentloaded' });
      return page.innerText('h1');
    },
    switchToiFrameBySelector: async (selector) => {
      const containerFrame = await page.waitForSelector(selector);
      const containerContent = await containerFrame.contentFrame();
      return containerContent;
    },
    // https://github.com/microsoft/playwright/issues/3166
    clickFrameElement: async (selector) => {
      const options = { position: { x: 50, y: 85 }, force: true };
      isHeadless
        ? await page.$eval(selector, (element) => element.click())
        : await page.click(selector, options);
    },
    getPageDimensions: async () =>
      page.evaluate(() => ({
        // eslint-disable-next-line no-undef
        width: document.documentElement.clientWidth,
        // eslint-disable-next-line no-undef
        height: document.documentElement.clientHeight,
      })),
    waitForResponse: async (url, opts) => {
      try {
        const response = await pageInstance.waitForResponse((res) => {
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

module.exports = actions;
