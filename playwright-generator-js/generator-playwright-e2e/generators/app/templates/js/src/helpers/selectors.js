/*
  Selector helper methods
*/
exports.byText = (text) => `//*[contains(text(),"${text}")]`;
exports.byTagAndText = (tag, text) => `//${tag}[contains(text(),"${text}")]`;
exports.byPlaceholder = (text) => `[placeholder="${text}"]`;
exports.buttonByText = (text) => `//button[contains(text(),"${text}")]`;
exports.linkByText = (text) => `//a[contains(text(),"${text}")]`;
