const crypto = require("crypto");


module.exports = {
    /**
     * Returns a random number between min (inclusive) and max (exclusive)
     */
     randomInt(min, max) {
        min = Math.ceil(min);
        max = Math.floor(max);
        return Math.floor(Math.random() * (max - min + 1)) + min;
    },

    /**
     * Returns an almost random(expect collisions) GUID
     */
     randomGuid() {
        let guid = crypto.randomBytes(16).toString("hex");
        return guid
    },
}
