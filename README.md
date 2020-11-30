# Muncipal-Tax-Calculation
DanskeIT Muncipal Tax Calculation Case Study

Assumptions: Daily tax is been taken on priority and then weekely, monthly and yearly. Based on muncipality name and date passed as an input it will call the get api and will calculate the tax.
Post endpoints are created to insert new records for muncipality taxes.

For reading data to calculate tax and storing data, In-memory cache is used instead of Database. So to set data in In-memory cache first need to call the Post endpoint and then need to call get endpoint.
Values are send to API in Json format.

Postman collection is added in a project.

Unit test cases are written and performed.
