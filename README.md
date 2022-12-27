# Exam-CPHMTOGO

## The Sample Scenario
CPHMTOGO's customers can utilize their accounts with the company to place orders for food remotely through online platforms, a mobile app, or a telephone ordering service. The food offered for delivery is either prepared by affiliated restaurants or by CPHMTOGO's own brand of food establishments, following their own recipes. The menu can be viewed on the CPHMTOGO website or through the mobile app, though CPHMTOGO does not have any input in the selection of items offered. Restaurants pay a fee to CPHMTOGO for access to the service, as well as a variable share of the total order value before VAT, based on certain rules such as the size of the order. Customers do not pay a delivery charge, so all menu prices include free delivery. CPHMTOGO employs local agents to collect and deliver the food. Following a recent court case, these agents are now considered employees and receive bonuses based on the value of orders delivered, as well as their punctuality and customer reviews. Customers can track the progress of their orders through SMS updates or the CPHMTOGO app. Payment processing is handled by an external supplier, and delivery agents do not handle payment at all. After delivery, customers are invited to provide feedback on the food, overall experience, and individual delivery person. Management has access to a dashboard displaying order status data such as the number of open orders and average processing time. Currently, there is no system in place for handling customer or supplier complaints within the application, though there are plans to implement this feature in the future. To meet performance requirements and support growth, the application must be able to scale to accommodate an increase in the customer base from 300,000 to 1.5 million people and orders from 3.6 million to 18 million over the next five years.

asd

## Members
| Name                        | Mail                     |
|-----------------------------|--------------------------|
| Benjamin Ćurović            | cph-bc121@cphbusiness.dk |
| Abdelhamid Hariri           | cph-ah482@cphbusiness.dk |
| Jonas Ancker Juul Jørgensen |                          |

## How to run the project
Prerequisite:
You must have Docker to run the program

To initiate the project, the start-bash.bat file located within the Exam-CPHMTOGO Folder must be run within the command prompt.

Upon completion of the process, the API Gateway can be executed with all functionalities.

## ER Diagram
![](https://raw.githubusercontent.com/Abed01-lab/prisma-erd/b9fb5f2de610b67b6c673ad1b352c69996e4f22b/prisma/ERD.svg)


## SOA
Our system is based on Service-Oriented Architecture (SOA), which requires us to adhere to certain principles. These principles are shared information and semantics, application integration, service reuse and governance, and management.

**Shared Information and Semantics:**

CPHMTOGO can document processing capabilities due to its capacity to convert both typed and handwritten data into structured, machine-readable data. This capability is further enhanced through integration with existing enterprise information models, such as microservices, which are frequently utilized in our project. We have carefully outlined the information model in an entity-relationship diagram.

**Application Integration:**

CPHMTOGO offers functions and data as services, utilizing stripe as a legacy model to facilitate the transformation between the enterprise model and the legacy model. These services are not directly accessible to the user, but rather are made available through the API Gateway. Both of our business processes are designed to operate through an integrated system rather than being directly connected to a database or legacy system. This approach enables us to ensure the seamless integration and management of these processes.

**Service Reuse and Governance:**

CPHMTOGO maintains a centralized repository of services that can be accessed and utilized by various departments within the organization. To ensure the efficient and effective utilization of these services, the company has implemented a system for monitoring their usage through logging and tracking the frequency of their utilization. This enables the company to optimize the usage of these resources and identify any potential areas for improvement.

**Management:**

CPHMTOGO utilizes a centralized system for managing the various services that are employed throughout the organization, specifically the API Gateway, which serves as the primary point of access and management for these resources. This enables the organization to effectively oversee and optimize the usage of these services, ensuring that they are utilized in a manner that is consistent with the needs and goals of the organization.


(Versioning and lifecycle aren't used in this project)
## API's
| Service                 | Technologies        | Local Development | From other docker containers        |
|-------------------------|---------------------|-------------------|-------------------------------------|
| API Gateway             | REST, C#            | localhost:5000    |                                     |
| Address Service         | REST, C#            | localhost:4015    |  cphmtogo-address-service           |
| Authentication Service  | gPRC, C#            | localhost:4011    |  cphmtogo-authentication-service    |
| Message Broker          | TypeScript          | localhost:6000    |                                     |
| Order Service           | REST, C#            | localhost:4012    |  cphmtogo-order-service             |
| Payment Logging Service | REST, C#            | localhost:4014    |  cphmtogo-paymentlogging-service    |
| Payment Service         | REST, C#            | localhost:4013    |  cphmtogo-payment-service           |
| Restaurant Service      | GraphQL, TypeScript | localhost:4000    |  cphmtogo-restaurant-service        |
| User Service            | REST, C#            | localhost:3000    |  cphmtogo-user-service              |

## Languages
In the CPHMTOGO project, the decision was made to utilize both Typescript and C# due to the various benefits that these languages offer. The use of multiple languages has provided increased flexibility, access to a wider range of libraries and frameworks, and facilitated easier maintenance. Additionally, this approach simulated a realistic scenario in which larger teams often work with different programming languages, and allowed team members to utilize their strengths by using the language that they feel most comfortable with. Overall, the incorporation of multiple languages has proven to be a valuable asset in the CPHMTOGO project.

## BPM
Ordering Process:
![](https://github.com/Benjo6/Exam-CPHMTOGO/blob/main/BPM/OrderBPM.png)

Employee Delivering Process:
![](https://github.com/Benjo6/Exam-CPHMTOGO/blob/main/BPM/DeliveringProcess.png)

## RabbitMQ

We are using RabbitMQ beacuase we have processes that is required when a specific event happens. This is called Event Driven. They way we use it is to automate the process of sending emails to customers when the make an order. We have created a queue in RabbitMQ called order, and we have a consumer listining on the queue. Whenever a message is sent to the queue, the consumer does something to with that message. We use the message, which is a JSON object with the information about the order, to send out an appropriate email to the customer about the status of the order.
 
## Maturity Levels of REST API Design
It can be stated that CPHMTOGO exhibits a level of sophistication in its design as it satisfies the criteria for the maturity levels of REST API design. This suggests that the API is structured in a way that is coherent, easy to use, and adaptable, making it a valuable resource for developers seeking to incorporate it into their applications.

Level 0: It expose a database through a CRUD interface
The API provides create, read, update, and delete (CRUD) operations for a database of customer records.

Level 1: It use HTTP verbs to operate on the resources
The API uses the appropriate HTTP verbs (GET, POST, PUT, DELETE) for each CRUD operation.

Level 2: It use HTTP status codes to indicate success or failure
The API returns appropriate HTTP status codes (e.g. 200 OK, 404 Not Found) to indicate the success or failure of each request.

Level 3: It use hypermedia controls to expose the API's capabilities
The API includes links in the responses that allow the client to discover and navigate to other resources and actions.

Level 4: It use an API gateway and microservices architecture
The API is designed as a set of microservices, each with a specific responsibility, and is accessed through an API gateway that routes requests to the appropriate microservice and handles authentication and authorization.

By adhering to the principles and constraints that define the maturity levels of REST API design, this API exhibits a level of polish and professionalism that makes it an ideal choice for developers seeking to utilize it in their projects. Its well-organized structure, user-friendly design, and versatility make it a highly valuable resource for building effective applications.







