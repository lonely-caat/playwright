module.exports = {
    required(variableName) {
        throw new Error(`argument ${variableName} is required. `);
    }
}