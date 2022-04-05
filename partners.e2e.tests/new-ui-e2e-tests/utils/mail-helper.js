require('dotenv-safe').config();
const apiHelper = require('./api-helper')
const GetUrl = require('get-hrefs');


class MailHelper {

  async createUser() {
    const timeStamp = new Date().getTime();
    let user = `${timeStamp}_cc`;
    return user;
  }

  async getEmailDetails(userId, subject, wait_time=9000) {
    return await apiHelper.getMailinatorEmailBySubject(userId, subject, wait_time);
  }

  async getURL(mailContent, linkText) {
    const urlTag = mailContent
      .match(/<a[^>]*>((?:.|\r?\n)*?)<\/a>/g)
      .filter((url) => url.indexOf(linkText) !== -1);
    let urlT = GetUrl(urlTag[0]);
    let url = decodeURIComponent(urlT[0]).trim();
    let urlIndex = url.indexOf("\"");
    let urlLenth = url.length;
    url = url.substring(urlIndex + 1, urlLenth-2);
    return url;
  }
  async getURLResetPassword(mailContent) {
    let content = mailContent.replace(/\n|\r/g, "");
    let regex = /^.*ticket.*$/g;
    let element;
    let url;
    for (element of content.split(' ')){if (element.match(regex)){url = element}};
    return url;
  }
}

module.exports = new MailHelper();
