**Title:** Document Generator Service

**Description:**

The requirement is to build a Document Generator service.

The service will accept a JSON request and return one of the following:

-   A downloadable link to a PDF
-   The PDF file itself

The service will maintain dynamic templates that can be populated with information from the JSON request to generate the document.

Implement a solution that fulfills the following requirements:

-   The API should support document types of 'idd', 'dan'. Those stand for Initial disclosure document and Demands and needs document. It should be designed to easily accommodate more document types in the future.
-   A user can make an HTTP request and receive a PDF file as a direct response.
-   A user can make a HTTP request and be returned a downloadable link to a PDF file.
-   Each request can hold different user information so templates have to be dynamic
-   All code completed has to be tested and have code coverage
-   Should be able to execute from any machine
-   README.MD should have clear instructions on how to test the completed project

You have complete freedom in structuring your project and choosing libraries to achieve this test as we will be reviewing how you structure the project and the code.

**Project Presets:**

You are provided with a folder containing example templates that you are expected to make dynamic. You will also find a folder with example JSON requests that will be required to pass through your API.

If you don't have enough time to complete the test, getting so far and describing below how you would achieve the rest will be sufficient.

**Specific Requirements and Considerations:**

1.  **Document Generation**
2.  **API Design**
3.  **File Handling**
4.  **Error Handling**
5.  **Testing**
6.  **Security**


**Submission**
You will need to fork or clone this repo and send us a link to your forked/cloned version.
