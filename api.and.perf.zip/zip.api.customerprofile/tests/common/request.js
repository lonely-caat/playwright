module.exports=(email) => {
    return `{
        customerProfile(keyword: "${email}") {
            id
            email
          }
        }`
}