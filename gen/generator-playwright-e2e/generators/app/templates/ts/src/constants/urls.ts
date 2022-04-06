import { isDev } from '../config';

const env = isDev ? 'dev' : 'sandbox';
export const host = `https://${env}.zip.co`;

const urls = {
  accountSelector: `${host}/customer/account-selector`,
  customer: `${host}/customer/`,
  giftcards: `${host}/giftcards`,
  merchantMadison: 'http://10.41.10.74/elizabeth-knit-top-596.html',
  merchantLuma: 'http://10.41.10.23/radiant-tee.html',
};

export default urls;
