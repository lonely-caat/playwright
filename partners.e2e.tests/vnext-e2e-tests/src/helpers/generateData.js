
module.exports = {
  createRandomEmail() {
    const random_string = Math.random().toString(36).substring(2, 15)
    return `bas-testing-${random_string}@zipteam222898.testinator.com`;
  },

};
