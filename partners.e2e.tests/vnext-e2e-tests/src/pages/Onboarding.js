const { ONBOARDING } = require('../constants/onboardingData')


const OnboardingPage = (page) => {
    const elements = {
        header: 'h1',
        header2: 'h2',
        firstName: '#mat-input-0',
        lastName: '#mat-input-1',
        email: '#mat-input-2',
        phone: '#mat-input-3',
        agreementCheckbox: '#mat-checkbox-1 div',
        continueButton: 'button:has-text("Continue")',
        submitButton: 'button:has-text("Submit")',

        abn: '#mat-input-4',
        tradingName: '#mat-input-5',
        industryDropDown: '#industryInput',
        entityName: '#businessName',

        businessDropDown: '//span[contains(text(), "Please select")]',
        onlinePayments: '//span[contains(text(), "Online payments")]',
        instorePayments: '//span[contains(text(), "Instore payments")]',
        bothPayments: '//span[contains(text(), "Both")]',

        anualSales: '#instoreRevenue',
        anualOnlineSales: '#onlineRevenue',
        anualSalesLow: '//span[contains(text(), "$0 - $250,000")]',
        anualSalesMid: '//span[contains(text(), "$1M - $5M")]',
        anualSalesHigh: '//span[contains(text(), "$10M+")]',

        transactionValue: '#averageTransactionValue',
        transactionValueLow: '//span[contains(text(), "$0 - $1000")]',
        transactionValueMid: '//span[contains(text(), "$1000 - $3000")]',
        transactionValueHigh: '//span[contains(text(), "$3000+")]',

        totalOnlineSales: '#mat-select-20',

        shippingTime: '#shippingTime',
        shippingTimeLow: '//span[contains(text(), "0-7 days")]',
        shippingTimeMid: '//span[contains(text(), "8-14 days")]',
        shippingTimeHigh: '//span[contains(text(), "30+ days")]',

        eCommercePlatform: '#eCommercePlatform',
        eCommerceBadPlatformShopify: '//span[contains(text(), "Shopify")]',
        eCommerceBadPlatformBigCommerce: '//span[contains(text(), "Big Commerce")]',
        eCommerceGoodPlatformMagento: '//span[contains(text(), "Magento")]',

        businessWebsiteUrl: 'input[type="url"]',

        confirmRefundPolicyCheckBox: '#mat-checkbox-2 div',
        confirmShippingPolicyCheckBox: '#mat-checkbox-3 div',

        yesService: '//mat-radio-button[@ng-reflect-value="Yes"]',
        noService: '//mat-radio-button[@ng-reflect-value="No"]',
        serviceTimeDropDown: 'text=How long does the service take',
        serviceTimeLow: '//span[contains(text(), "Within 7 days")]',
        serviceTimeMid: '//span[contains(text(), "15-21 days")]',
        serviceTimeHigh: '//span[contains(text(), "More than 30 days")]',

        paymentTypeDropDown: 'text=How is payment taken?',
        paymentTypeBooking: '//span[contains(text(), "On booking")]',
        paymentTypeServiceComplete: '//span[contains(text(), "When service is complete")]',
        paymentTypeServiceDeposit: '//span[contains(text(), "With a deposit")]',

        businessAddress: '//input[@ng-reflect-name="searchAddress"]',
        businessMobile: '//input[@type="tel"]',
        manualAddress: '#manualAddressTrigger',
        addressLineOne: '#addressLineOne',
        addressLineTwo: '#addressLineTwo',
        suburb: '#suburb',
        postcode: '#postcode',
        stateDropDown: '#state',
        stateNSW: '//span[contains(text(), "NSW")]',
        stateQLD: '//span[contains(text(), "QLD")]',
        stateWA: '//span[contains(text(), "WA")]',


        storesAmountDropDown: '#numberOfLocations',
        storesAmountLow: '//span[contains(text(), "1")]',
        storesAmountMid: '//span[contains(text(), "2 - 5")]',
        storesAmountHigh: '//span[contains(text(), "More than 5")]',
        sameAsContactMobile: '//span[contains(text(), "Same as contact number")]',

        onboardingSuccessfull: '//div[contains(text(), "Great, your business is eligible for Zip")]',
        onboardingFailed: '//p[contains(text(), "Your application has been received and will be reviewed by our dedicated team.")]'
    };

    async function verifyOnboardingSuccessfull(expect_success=true){
        const { onboardingSuccessfull, onboardingFailed } = elements;

        if (expect_success) {
            await page.click(onboardingSuccessfull)
        }
        if (!expect_success) {
            await page.click(onboardingFailed)
        }
    }

    async function getHeaderText() {
        const { header } = elements;
        return page.innerText(header);
    }

    async function getH2Text() {
        const { header2 } = elements;
        return page.innerText(header2);
    }

    async function getSuccessMessage() {
        const { message } = elements;
        const success = await page.isVisible(message)
        return success
    }

    async function industryByText(text) {
        const { industryDropDown } = elements;
        await page.click(industryDropDown);
        await page.click(`text="${text}"`);
    }

    async function tellAboutYourself(customer_email,first_name='meow',
                                     last_name='Smith', phone_number='0400000000') {
        const { firstName, lastName, email, phone, agreementCheckbox, continueButton } = elements;
        await page.fill(firstName, first_name);
        await page.fill(lastName, last_name);
        await page.fill(email, customer_email);
        await page.fill(phone, phone_number);
        await page.click(agreementCheckbox);
        await page.click(continueButton);
    }

    async function tellAboutYourBusiness(industry='Airlines', abn_number= ONBOARDING.abn, trade_name='bastest') {
        const { abn, tradingName, continueButton } = elements;
        await page.fill(abn, abn_number);
        await page.fill(tradingName, trade_name);
        await industryByText(industry)
        await page.click(continueButton);
    }

    async function tellBusinessDetails(type='both') {
        const { businessDropDown, instorePayments, onlinePayments, bothPayments, continueButton  } = elements;

        await page.click(businessDropDown);
        switch(type) {
            case 'instore':
                await page.click(instorePayments);
                break;
            case 'online':
                await page.click(onlinePayments);
                break;
            case 'both':
                await page.click(bothPayments);
                break;
        }
        await page.click(continueButton);
    }

    async function tellBusinessDetailsInStore(sales_amount='high', transaction_value= 'high'){
        const { instorePayments, anualSales, anualSalesLow, anualSalesMid, anualSalesHigh, transactionValue,
                transactionValueLow, transactionValueMid, transactionValueHigh, businessDropDown,
                confirmShippingPolicyCheckBox, confirmRefundPolicyCheckBox, continueButton } = elements;

        await page.click(businessDropDown);
        await page.click(instorePayments);
        await page.click(anualSales);
        switch (sales_amount) {
            case 'low':
                await page.click(anualSalesLow);
                break;
            case 'mid':
                await page.click(anualSalesMid);
                break;
            case 'high':
                await page.click(anualSalesHigh);
                break;
        }
        await page.click(transactionValue);
        switch (transaction_value) {
            case 'low':
                await page.click(transactionValueLow);
                break;
            case 'mid':
                await page.click(transactionValueMid);
                break;
            case 'high':
                await page.click(transactionValueHigh);
                break;
        }
        await page.click(confirmShippingPolicyCheckBox);
        await page.click(confirmRefundPolicyCheckBox);
        await page.click(continueButton)
    }

    async function tellBusinessDetailsOnlineAndBoth(platform='both',e_commerce_platform='magento',
                                                    sales_amount='high', shipping_time= 'low',
                                                    transaction_value='high',
                   business_website='bas.likes.testing.com'){
        const { onlinePayments, bothPayments, anualOnlineSales, anualSales, anualSalesLow, anualSalesMid,
                anualSalesHigh, shippingTime, shippingTimeLow, shippingTimeMid, shippingTimeHigh, transactionValue,
                transactionValueLow, transactionValueMid, transactionValueHigh, eCommercePlatform, businessDropDown,
                eCommerceGoodPlatformMagento, eCommerceBadPlatformBigCommerce, eCommerceBadPlatformShopify,
                businessWebsiteUrl, confirmShippingPolicyCheckBox, confirmRefundPolicyCheckBox, continueButton } = elements;

        await page.click(businessDropDown);
        platform === 'online' ? await page.click(onlinePayments) : await page.click(bothPayments)
        platform === 'online' ? await page.click(anualOnlineSales) : await page.click(anualSales)
        switch (sales_amount) {
            case 'low':
                await page.click(anualSalesLow);
                break;
            case 'mid':
                await page.click(anualSalesMid);
                break;
            case 'high':
                await page.click(anualSalesHigh);
                break;
        }

        await page.click(shippingTime)
        switch (shipping_time) {
            case 'low':
                await page.click(shippingTimeLow);
                break;
            case 'mid':
                await page.click(shippingTimeMid);
                break;
            case 'high':
                await page.click(shippingTimeHigh);
                break;
        }

        await page.click(transactionValue)
        switch (transaction_value) {
            case 'low':
                await page.click(transactionValueLow);
                break;
            case 'mid':
                await page.click(transactionValueMid);
                break;
            case 'high':
                await page.click(transactionValueHigh);
                break;
        }

        await page.click(eCommercePlatform)
        switch (e_commerce_platform) {
            case 'magento':
                await page.click(eCommerceGoodPlatformMagento);
                break;
            case 'bigcommerce':
                await page.click(eCommerceBadPlatformBigCommerce);
                break;
            case 'shopify':
                await page.click(eCommerceBadPlatformShopify);
                break;
        }
        await page.fill(businessWebsiteUrl, business_website);
        await page.click(confirmRefundPolicyCheckBox);
        await page.click(confirmShippingPolicyCheckBox);
        await page.click(continueButton)
    }

    async function tellBusinessService (service=false, service_time='low', service_payment='booking'){
        const { noService, yesService, serviceTimeDropDown, serviceTimeLow, serviceTimeMid, serviceTimeHigh,
                paymentTypeDropDown, paymentTypeBooking, paymentTypeServiceComplete, paymentTypeServiceDeposit,
                continueButton } = elements;

        if (!service){
            page.click(noService);
        }
        else {
            page.click(yesService);
            page.click(serviceTimeDropDown);
            switch (service_time) {
                case 'low':
                    await page.click(serviceTimeLow);
                    break;
                case 'mid':
                    await page.click(serviceTimeMid);
                    break;
                case 'high':
                    await page.click(serviceTimeHigh);
                    break;
            }
            page.click(paymentTypeDropDown)
            switch (service_payment) {
                case 'booking':
                    await page.click(paymentTypeBooking);
                    break;
                case 'complete':
                    await page.click(paymentTypeServiceComplete);
                    break;
                case 'deposit':
                    await page.click(paymentTypeServiceDeposit);
                    break;
            }
        }
        page.click(continueButton);
    }

    async function setBusinessAddressManually(instore_n_both=required('boolean'), address_line_one='123 Catspaw Avenue',
                                              address_line_two= 'Sydney', sub_urb= 'Beeliar', post_code= '6164',
                                              state='WA', stores_amount='high', phone_number= '0400000000'){
        const { manualAddress, addressLineOne, addressLineTwo, suburb, postcode, stateDropDown, storesAmountDropDown,
                storesAmountLow, storesAmountMid, storesAmountHigh, businessMobile, submitButton } = elements;

        if (instore_n_both) {
            await page.click(storesAmountDropDown)
            switch (stores_amount) {
                case 'low':
                    await page.click(storesAmountLow);
                    break;
                case 'mid':
                    await page.click(storesAmountMid);
                    break;
                case 'high':
                    await page.click(storesAmountHigh);
                    break;
            }
        }

        await page.click(manualAddress);
        await page.pause()
        await page.fill(addressLineOne, address_line_one);
        await page.fill(addressLineTwo, address_line_two);
        await page.fill(suburb, sub_urb);
        await page.fill(postcode, post_code);

        await page.click(stateDropDown);
        await page.click(`//span[contains(text(), "${state}")]`);
        await page.fill(businessMobile, phone_number);
        await page.click(submitButton);

    }

    async function tellBusinessAddress(instore_n_both=required('boolean'), stores_amount='high',
                                       phone_number='0400000000', business_address='123 Catspaw Avenue, Beeliar WA, Australia' ) {
        const { storesAmountDropDown, storesAmountLow, storesAmountMid, storesAmountHigh, businessAddress,
                businessMobile, submitButton } = elements;

        if (instore_n_both) {
            await page.click(storesAmountDropDown)
            switch (stores_amount) {
                case 'low':
                    await page.click(storesAmountLow);
                    break;
                case 'mid':
                    await page.click(storesAmountMid);
                    break;
                case 'high':
                    await page.click(storesAmountHigh);
                    break;
            }
        }
        await page.type(businessAddress, business_address);
        // Hack for stupid google maps drop-down
        await page.waitForTimeout(1000);
        await page.click('h2');
        await page.click(businessAddress);
        await page.click('text=Australia');

        await page.fill(businessMobile, phone_number);
        await page.click(submitButton)
    }



    return {
        getHeaderText,
        getH2Text,
        getSuccessMessage,
        tellAboutYourself,
        tellAboutYourBusiness,
        tellBusinessDetails,
        tellBusinessDetailsInStore,
        tellBusinessDetailsOnlineAndBoth,
        tellBusinessService,
        tellBusinessAddress,
        setBusinessAddressManually,
        verifyOnboardingSuccessfull,


    };
};

module.exports = OnboardingPage;

