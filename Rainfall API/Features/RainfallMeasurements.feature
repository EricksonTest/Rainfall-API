Feature: Rainfall Measurements API

    Scenario: Request rainfall data with a limit
        Given I have a valid station identifier "1029TH"
        When I request rainfall measurements with a limit of "5"
        Then I should receive no more than "5" measurements

    Scenario: Validate measurement dates
        Given I have a valid station identifier "1029TH"
        When I request rainfall measurements for the date "2024-02-01"
        Then all returned measurements should have the date "2024-02-01"