export default class Helpers{
    // TODO: figure out what goes into this file and what goes into commands file

    static generateValidLogin(length=5, emailDomain='gmail.com'){
        const emailBody = [...Array(length)].map(() => Math.random().toString(36)[2]).join('')
        return `${emailBody}@${emailDomain}`
    }
    static generateValidPassword(length=5){
        return [...Array(length)].map(() => Math.random().toString(36)[2]).join('')
    }
}