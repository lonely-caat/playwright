namespace Zip.Api.MerchantDataEnrichment.Test.UnitTests.Infrastructure.MerchantLookupServiceTests
{
    public static class ResponseConstants
    {
        public const string SampleResponse1 = @"
{
    ""transactions_count"": 2,
    ""search_time_ms"": 7,
    ""total_credits_used"": 10,
    ""fields"": ""primary_address,primary_contacts,primary_category,legal_business_info,merchant_logo"",
    ""search_results"": [
        {
            ""search_guid"": ""3a38bef3-9553-40bd-a993-df4de198246f"",
            ""cal"": ""Hungry Jacks Circular Quay"",
            ""transaction_correlation_guid"": ""71c13922-0b32-46c7-8499-29cfaa9d5161"",
            ""number_of_results"": 1,
            ""highest_score"": 99.0,
            ""utc_expiry_time"": ""2020-11-17T05:32:24.6353266Z"",
            ""merchant_search_results"": [
                {
                    ""search_results_guid"": ""58ac6207-26b2-4277-a3b9-8c3e8efb42f1"",
                    ""lwc_guid"": ""3898ebd8-8bcf-4e34-a82a-e51ce227e6de"",
                    ""rank"": 1,
                    ""score"": 99.0,
                    ""match_feedback_requested"": false,
                    ""type_of_match"": ""KNOWN"",
                    ""merchant_details"": {
                        ""lwc_guid"": ""3898ebd8-8bcf-4e34-a82a-e51ce227e6de"",
                        ""data_enrichment_score"": 100,
                        ""merchant_primary_name"": ""Hungry Jacks (Circular Quay)"",
                        ""primary_address"": {
                            ""single_line_address"": ""E1 Alfred St, Sydney NSW 2000, Australia"",
                            ""address_line_1"": ""E1 Alfred St"",
                            ""state"": ""NSW"",
                            ""postcode"": ""2000"",
                            ""suburb"": ""Sydney"",
                            ""longitude"": 151.211014,
                            ""latitude"": -33.861333,
                            ""lat_lon_precision"": 9,
                            ""mapable"": true,
                            ""street_view_available"": false
                        },
                        ""primary_contacts"": [
                            {
                                ""type_of_contact"": ""PHONE"",
                                ""value"": ""(02) 9241 3375"",
                                ""display_value"": ""(02) 9241 3375"",
                                ""label"": ""Phone""
                            },
                            {
                                ""type_of_contact"": ""EMAIL"",
                                ""value"": ""hja.customerservice@hungryjacks.com.au"",
                                ""display_value"": ""hja.customerservice@hungryjacks.com.au"",
                                ""label"": ""Email""
                            },
                            {
                                ""type_of_contact"": ""URL"",
                                ""value"": ""https://www.hungryjacks.com.au/home"",
                                ""display_value"": ""hungryjacks.com.au"",
                                ""label"": ""Web""
                            }
                        ],
                        ""primary_category"": {
                            ""category_name"": ""Fast Food"",
                            ""id"": 3001,
                            ""full_category_path"": ""Food/Drink >> Fast Food"",
                            ""parent"": {
                                ""category_name"": ""Food/Drink"",
                                ""id"": 3,
                                ""full_category_path"": ""Food/Drink""
                            },
                            ""is_sensitive"": false,
                            ""is_lwc_category"": true,
                            ""is_substituted_category"": false,
                            ""lwc_category_icon"": {
                                ""Black_white_url"": ""https://images.lookwhoscharging.com/categoryicons/bw_3001.png"",
                                ""Coloured_url"": ""https://images.lookwhoscharging.com/categoryicons/clr_3001.png"",
                                ""height"": 128,
                                ""width"": 128
                            },
                            ""lwc_category_icon_circular"": {
                                ""Black_white_url"": ""https://images.lookwhoscharging.com/categoryicons/bw_3001.c.png"",
                                ""Coloured_url"": ""https://images.lookwhoscharging.com/categoryicons/clr_3001.c.png"",
                                ""height"": 128,
                                ""width"": 128
                            },
                            ""category_emoji"": ""U+1F354""
                        },
                        ""merchant_logo"": {
                            ""url"": ""https://images.lookwhoscharging.com/3898ebd8-8bcf-4e34-a82a-e51ce227e6de/Hungry-Jacks-Circular-Quay-logo-image.png"",
                            ""height"": 128,
                            ""width"": 128
                        },
                        ""last_updated"": ""2020-09-19T14:52:00Z"",
                        ""legal_business_info"": {
                            ""date_established"": ""2000-01-01T00:00:00Z"",
                            ""entity_type"": ""PRV"",
                            ""current_merchant_status"": ""Active"",
                            ""merchant_type"": ""Branch"",
                            ""merchant_presence"": ""Bricks & Mortar"",
                            ""chain_name"": ""Hungry Jacks"",
                            ""legal_registrations"": [
                                {
                                    ""legal_number"": ""008747073"",
                                    ""legal_number_label"": ""ACN""
                                },
                                {
                                    ""legal_number"": ""25008747073"",
                                    ""legal_number_label"": ""ABN""
                                }
                            ],
                            ""registered_for_sales_tax"": true,
                            ""chain_lwc_guid"": ""bb785ff5-59a3-4fcc-be5c-a5f35b1ff7d6""
                        }
                    }
                }
            ],
            ""user_message"": ""The search for 'Hungry Jacks Circular Quay' returned 1 merchant"",
            ""system_message"": ""The search for 'Hungry Jacks Circular Quay' returned 1 merchant"",
            ""result_code"": 10000,
            ""lwc_attempting_to_index"": false,
            ""transaction_search_time_ms"": 1,
            ""credits_used"": 5
        }
    ]
}
";

        public const string SampleResponse2 = @"
{
    ""transactions_count"": 2,
    ""search_time_ms"": 7,
    ""total_credits_used"": 10,
    ""fields"": ""primary_address,primary_contacts,primary_category,legal_business_info,merchant_logo"",
    ""search_results"": [
        {
            ""search_guid"": ""14fe8018-35f0-4f6c-bfc1-775b18db6f5b"",
            ""cal"": ""JFC STRATHFIELD PL STRATHFIELD"",
            ""transaction_correlation_guid"": ""71c13922-0b32-46c7-8499-29cfaa9d5160"",
            ""number_of_results"": 1,
            ""highest_score"": 99.0,
            ""utc_expiry_time"": ""2020-11-17T05:33:27.2574319Z"",
            ""merchant_search_results"": [
                {
                    ""search_results_guid"": ""17623cb8-56af-4f35-9fae-fdea3e783bfb"",
                    ""lwc_guid"": ""de781af5-fed3-484a-8eae-ecaf81f16c41"",
                    ""rank"": 1,
                    ""score"": 99.0,
                    ""match_feedback_requested"": false,
                    ""type_of_match"": ""KNOWN"",
                    ""merchant_details"": {
                        ""lwc_guid"": ""de781af5-fed3-484a-8eae-ecaf81f16c41"",
                        ""data_enrichment_score"": 65,
                        ""merchant_primary_name"": ""Pappa Roti Strathfield"",
                        ""primary_address"": {
                            ""single_line_address"": ""Strathfield Plaza, Shop 49/11 The Boulevarde, Strathfield NSW 2135, Australia"",
                            ""address_line_1"": ""11 The Boulevarde"",
                            ""state"": ""NSW"",
                            ""postcode"": ""2135"",
                            ""suburb"": ""Strathfield"",
                            ""longitude"": 151.093177,
                            ""latitude"": -33.873009,
                            ""lat_lon_precision"": 9,
                            ""mapable"": true,
                            ""street_view_available"": false
                        },
                        ""primary_contacts"": [
                            {
                                ""type_of_contact"": ""PHONE"",
                                ""value"": ""(02) 9746 1126"",
                                ""display_value"": ""(02) 9746 1126"",
                                ""label"": ""Phone""
                            }
                        ],
                        ""primary_category"": {
                            ""category_name"": ""Cafe"",
                            ""id"": 3999002,
                            ""full_category_path"": ""Food/Drink >> Other >> Cafe"",
                            ""parent"": {
                                ""category_name"": ""Other"",
                                ""id"": 3999,
                                ""full_category_path"": ""Food/Drink >> Other"",
                                ""parent"": {
                                    ""category_name"": ""Food/Drink"",
                                    ""id"": 3,
                                    ""full_category_path"": ""Food/Drink""
                                }
                            },
                            ""is_sensitive"": false,
                            ""is_lwc_category"": true,
                            ""is_substituted_category"": false,
                            ""lwc_category_icon"": {
                                ""Black_white_url"": ""https://images.lookwhoscharging.com/categoryicons/bw_3999002.png"",
                                ""Coloured_url"": ""https://images.lookwhoscharging.com/categoryicons/clr_3999002.png"",
                                ""height"": 128,
                                ""width"": 128
                            },
                            ""lwc_category_icon_circular"": {
                                ""Black_white_url"": ""https://images.lookwhoscharging.com/categoryicons/bw_3999002.c.png"",
                                ""Coloured_url"": ""https://images.lookwhoscharging.com/categoryicons/clr_3999002.c.png"",
                                ""height"": 128,
                                ""width"": 128
                            },
                            ""category_emoji"": ""U+2615""
                        },
                        ""last_updated"": ""2020-10-14T07:51:00Z"",
                        ""legal_business_info"": {
                            ""date_established"": ""2018-01-01T00:00:00Z"",
                            ""entity_type"": ""PRV"",
                            ""current_merchant_status"": ""Closed"",
                            ""merchant_type"": ""Stand-Alone"",
                            ""merchant_presence"": ""Bricks & Mortar"",
                            ""legal_registrations"": [
                                {
                                    ""legal_number"": ""626989259"",
                                    ""legal_number_label"": ""ACN""
                                },
                                {
                                    ""legal_number"": ""80626989259"",
                                    ""legal_number_label"": ""ABN""
                                }
                            ],
                            ""registered_for_sales_tax"": true
                        }
                    }
                }
            ],
            ""user_message"": ""The search for 'JFC STRATHFIELD PL STRATHFIELD' returned 1 merchant"",
            ""system_message"": ""The search for 'JFC STRATHFIELD PL STRATHFIELD' returned 1 merchant"",
            ""result_code"": 10000,
            ""lwc_attempting_to_index"": false,
            ""transaction_search_time_ms"": 2,
            ""credits_used"": 5
        },
        {
            ""search_guid"": ""0bd96790-95ba-419a-8bbc-d3c1c5ec3fa3"",
            ""cal"": ""Hungry Jacks Circular Quay"",
            ""transaction_correlation_guid"": ""71c13922-0b32-46c7-8499-29cfaa9d5161"",
            ""number_of_results"": 1,
            ""highest_score"": 99.0,
            ""utc_expiry_time"": ""2020-11-17T05:33:27.2574405Z"",
            ""merchant_search_results"": [
                {
                    ""search_results_guid"": ""aed05c41-3f98-4853-8bbe-7cb033c2cff8"",
                    ""lwc_guid"": ""3898ebd8-8bcf-4e34-a82a-e51ce227e6de"",
                    ""rank"": 1,
                    ""score"": 99.0,
                    ""match_feedback_requested"": false,
                    ""type_of_match"": ""KNOWN"",
                    ""merchant_details"": {
                        ""lwc_guid"": ""3898ebd8-8bcf-4e34-a82a-e51ce227e6de"",
                        ""data_enrichment_score"": 100,
                        ""merchant_primary_name"": ""Hungry Jacks (Circular Quay)"",
                        ""primary_address"": {
                            ""single_line_address"": ""E1 Alfred St, Sydney NSW 2000, Australia"",
                            ""address_line_1"": ""E1 Alfred St"",
                            ""state"": ""NSW"",
                            ""postcode"": ""2000"",
                            ""suburb"": ""Sydney"",
                            ""longitude"": 151.211014,
                            ""latitude"": -33.861333,
                            ""lat_lon_precision"": 9,
                            ""mapable"": true,
                            ""street_view_available"": false
                        },
                        ""primary_contacts"": [
                            {
                                ""type_of_contact"": ""PHONE"",
                                ""value"": ""(02) 9241 3375"",
                                ""display_value"": ""(02) 9241 3375"",
                                ""label"": ""Phone""
                            },
                            {
                                ""type_of_contact"": ""EMAIL"",
                                ""value"": ""hja.customerservice@hungryjacks.com.au"",
                                ""display_value"": ""hja.customerservice@hungryjacks.com.au"",
                                ""label"": ""Email""
                            },
                            {
                                ""type_of_contact"": ""URL"",
                                ""value"": ""https://www.hungryjacks.com.au/home"",
                                ""display_value"": ""hungryjacks.com.au"",
                                ""label"": ""Web""
                            }
                        ],
                        ""primary_category"": {
                            ""category_name"": ""Fast Food"",
                            ""id"": 3001,
                            ""full_category_path"": ""Food/Drink >> Fast Food"",
                            ""parent"": {
                                ""category_name"": ""Food/Drink"",
                                ""id"": 3,
                                ""full_category_path"": ""Food/Drink""
                            },
                            ""is_sensitive"": false,
                            ""is_lwc_category"": true,
                            ""is_substituted_category"": false,
                            ""lwc_category_icon"": {
                                ""Black_white_url"": ""https://images.lookwhoscharging.com/categoryicons/bw_3001.png"",
                                ""Coloured_url"": ""https://images.lookwhoscharging.com/categoryicons/clr_3001.png"",
                                ""height"": 128,
                                ""width"": 128
                            },
                            ""lwc_category_icon_circular"": {
                                ""Black_white_url"": ""https://images.lookwhoscharging.com/categoryicons/bw_3001.c.png"",
                                ""Coloured_url"": ""https://images.lookwhoscharging.com/categoryicons/clr_3001.c.png"",
                                ""height"": 128,
                                ""width"": 128
                            },
                            ""category_emoji"": ""U+1F354""
                        },
                        ""merchant_logo"": {
                            ""url"": ""https://images.lookwhoscharging.com/3898ebd8-8bcf-4e34-a82a-e51ce227e6de/Hungry-Jacks-Circular-Quay-logo-image.png"",
                            ""height"": 128,
                            ""width"": 128
                        },
                        ""last_updated"": ""2020-09-19T14:52:00Z"",
                        ""legal_business_info"": {
                            ""date_established"": ""2000-01-01T00:00:00Z"",
                            ""entity_type"": ""PRV"",
                            ""current_merchant_status"": ""Active"",
                            ""merchant_type"": ""Branch"",
                            ""merchant_presence"": ""Bricks & Mortar"",
                            ""chain_name"": ""Hungry Jacks"",
                            ""legal_registrations"": [
                                {
                                    ""legal_number"": ""008747073"",
                                    ""legal_number_label"": ""ACN""
                                },
                                {
                                    ""legal_number"": ""25008747073"",
                                    ""legal_number_label"": ""ABN""
                                }
                            ],
                            ""registered_for_sales_tax"": true,
                            ""chain_lwc_guid"": ""bb785ff5-59a3-4fcc-be5c-a5f35b1ff7d6""
                        }
                    }
                }
            ],
            ""user_message"": ""The search for 'Hungry Jacks Circular Quay' returned 1 merchant"",
            ""system_message"": ""The search for 'Hungry Jacks Circular Quay' returned 1 merchant"",
            ""result_code"": 10000,
            ""lwc_attempting_to_index"": false,
            ""transaction_search_time_ms"": 2,
            ""credits_used"": 5
        }
    ]
}
";
    }
}
