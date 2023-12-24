# Quiddity is WIP

Quiddity is a framework for designing entities in the problem domain area of a software, in terms of Clean Architecture.
It aims at presenting an alternative DTO and ORM patterns by introducing live aggregating object printers and clever object communitation which minimizes repetition and data transfer.
Quiddity leverages GraphQL together with ZiZZI object printing capabilities to offer maximum flexibility in usage of the entity domain and usecase design. 

Any entity in software can be characterized by:
- One or more aspects that describe certain things about the entity
- Possible mutations of the entity

Entities often are bundled together by various aspects in clusters, which can
- Create new entities
- Remove entities
- Filter entities

