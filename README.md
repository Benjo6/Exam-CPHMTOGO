# Exam-CPHMTOGO

### The Sample Scenario
CPHMTOGO's customers can utilize their accounts with the company to place orders for food remotely through online platforms, a mobile app, or a telephone ordering service. The food offered for delivery is either prepared by affiliated restaurants or by CPHMTOGO's own brand of food establishments, following their own recipes. The menu can be viewed on the CPHMTOGO website or through the mobile app, though CPHMTOGO does not have any input in the selection of items offered. Restaurants pay a fee to CPHMTOGO for access to the service, as well as a variable share of the total order value before VAT, based on certain rules such as the size of the order. Customers do not pay a delivery charge, so all menu prices include free delivery. CPHMTOGO employs local agents to collect and deliver the food. Following a recent court case, these agents are now considered employees and receive bonuses based on the value of orders delivered, as well as their punctuality and customer reviews. Customers can track the progress of their orders through SMS updates or the CPHMTOGO app. Payment processing is handled by an external supplier, and delivery agents do not handle payment at all. After delivery, customers are invited to provide feedback on the food, overall experience, and individual delivery person. Management has access to a dashboard displaying order status data such as the number of open orders and average processing time. Currently, there is no system in place for handling customer or supplier complaints within the application, though there are plans to implement this feature in the future. To meet performance requirements and support growth, the application must be able to scale to accommodate an increase in the customer base from 300,000 to 1.5 million people and orders from 3.6 million to 18 million over the next five years.



### Members
| Name                        | Mail                     |
|-----------------------------|--------------------------|
| Benjamin Ćurović            | cph-bc121@cphbusiness.dk |
| Abdelhamid Hariri           |                          |
| Jonas Ancker Juul Jørgensen |                          |

## ER Diagram
![](https://raw.githubusercontent.com/Abed01-lab/prisma-erd/b9fb5f2de610b67b6c673ad1b352c69996e4f22b/prisma/ERD.svg)


### SOA
Our system is based on Service-Oriented Architecture (SOA), which requires us to adhere to certain principles. These principles are shared information and semantics, application integration, service reuse and governance, and management.

Shared Information and Semantics:
CPHMTOGO can document processing capabilities due to its capacity to convert both typed and handwritten data into structured, machine-readable data. This capability is further enhanced through integration with existing enterprise information models, such as microservices, which are frequently utilized in our project. We have carefully outlined the information model in an entity-relationship diagram.

Application Integration:

CPHMTOGO offers functions and data as services, utilizing stripe as a legacy model to facilitate the transformation between the enterprise model and the legacy model. These services are not directly accessible to the user, but rather are made available through the API Gateway. Both of our business processes are designed to operate through an integrated system rather than being directly connected to a database or legacy system. This approach enables us to ensure the seamless integration and management of these processes.

Service Reuse and Governance:

CPHMTOGO maintains a centralized repository of services that can be accessed and utilized by various departments within the organization. To ensure the efficient and effective utilization of these services, the company has implemented a system for monitoring their usage through logging and tracking the frequency of their utilization. This enables the company to optimize the usage of these resources and identify any potential areas for improvement.


Management:

CPHMTOGO utilizes a centralized system for managing the various services that are employed throughout the organization, specifically the API Gateway, which serves as the primary point of access and management for these resources. This enables the organization to effectively oversee and optimize the usage of these services, ensuring that they are utilized in a manner that is consistent with the needs and goals of the organization.


(Versioning and lifecycle aren't used in this project)
## API's

## Languages

## BPM
## RabbitMQ
## (optional) Hypermedia on reastfull levels
