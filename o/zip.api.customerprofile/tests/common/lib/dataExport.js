const customerProfileMapping = {
            local: require('../data/customerProfile/localData.json'),
            labs: require('../data/customerProfile/labsData.json'),
            dev: require('../data/customerProfile/devData.json'),
            stag: require('../data/customerProfile/stgData.json'),
            sand: require('../data/customerProfile/sandboxData.json'),
            perf: require('../data/customerProfile/perfData.json')
    },
    //customerProfileData=customerProfileMapping[Constants.currentEnvironement];
    customerProfileData=customerProfileMapping[process.env.environment];
    console.log ('Test Env: ',process.env.environment);

exports.customerProfileData=customerProfileData;