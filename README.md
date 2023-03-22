# PageTree

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;A tool for knowledge organization as a form of pages structured in tree-like method with highly interconnected and internavigatable elements for automatic, manually configured or based on spaced-repetition algorithm flashcards generation.

In the future could also contain features like paths, courses, lessons (a'ka Pluralsight), but of course with the previously mentioned tooling for material rehearsal it would make a great combo.

Once again in bulletpoints:
- structurized knowledge base of any domain - initially created for japanese language learning with potential to expand for other subjects
- automatically generated flashcards - depending on what level of the pages / knowledge tree you will navigate to and want the flashcard start to be generated from
- spaced repetition algorithm, proposing already searched or learned materials - all configurable
- target plaforms - web and mobile
- working online and offline

## Problem Description

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Preparing notes requires time and effort, which could be spend on just learning itself. Of course it does not have to be absolutely excluded from a learning process, since it's a way to learn by itself too. Although, why to waste time just to convert a knowledge from one type of media to the other, when it's just can be ready to consume.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Browsing, learning from the same notes either written down on a paper or a computer every day becomes so boring after a time.. It brings routine. It slowly makes you start to procrastinate again.

### Technical Documentation (not up-to-date)
https://github.com/Dariusz-L/PageTree-Design-Documentation

## Features

### Knowledge Structurization

#### Projects
- Create Project
- Update Project (name, description, etc.)
- Archive Project

#### Signatures
- Create Signature
- Rename Signature
- Change Index of Signature
- Delete Signature

#### Pages and Properties Manipulation
- Navigate To / Show Page
- Create Subpage
- Create Subpage From Template
- Create Link
- Rename Page
- Remove Property
- Change Signature of Page
- Change Index of Property
- Change Parent of Property

#### Page Templates
- Navigate To / Show Page
- Create Template Subpage
- Rename Template
- Rename Template Page
- Remove Template Page
- Change Signature of Template Page
- Change Index of Template Page
- Change Parent of Template Page

### Memorization & Repetition

#### Practice Categories
- Create Practice Category
- Rename Practice Category
- Change Index of Practice Category
- Delete Practice Category

#### Practice Tactics
- Create Practice Tactic
- Rename Practice Tactic
- Update Practice Tactic
- Change Index of Practice Tactic
- Delete Practice Tactic

#### Practice
- Generate Practice List

## Major Quality Attributes

--- 

## API

#### Users
| HTTP Method | Endpoint | Description |
| --- | --- | --- |
| `GET` | `api/v1/users/{id}` | Get a user by id |
| `GET` | `api/v1/users/me` | Get user by authentication token |
| `POST` | `api/v1/users` | Create a new user |

#### Projects
| HTTP Method | Endpoint | Description |
| :---: | --- | --- |
| `GET` | `api/projectUserLists/{id}` | Get a specific user list of projects |
| `POST` | `api/v1/projects` | Create a new project |
| `DELETE` | `api/projects/{id}` | Archive a project |
| `PUT` | `api/projects/{id}` | Update project details |

#### Signatures
| HTTP Method | Endpoint | Description |
| :---: | --- | --- |
| `GET` | `api/v1/projects/{projectID}/signatures` | Get all signatures of specific project |
| `POST` | `api/v1/signatures` | Create a new signature |
| `DELETE` | `api/v1/signatures/{id}` | Delete a signature |
| `PATCH` | `api/v1/signatures/{id}/changeName` | Change name of a signature |
| `PATCH` | `api/v1/signatures/{id}/changeIndex` | Change index of a signature |

#### Pages
| HTTP Method | Endpoint | Description |
| :---: | --- | --- |
| `GET` | `/api/pages/{id}` | Get a page |

#### Page Templates
| HTTP Method | Endpoint | Description |
| :---: | --- | --- |
| `GET` | `api/v1/projects/{projectID}/pageTemplates` | Get all page templates of a specific project |

#### Practice Categories
| HTTP Method | Endpoint | Description |
| :---: | --- | --- |
| `GET` | `api/v1/projects/{projectID}/practiceCategories` | Get all practice Categories of a specific project |

#### Practice Tactics
| HTTP Method | Endpoint | Description |
| :---: | --- | --- |
| `GET` | `api/v1/projects/{projectID}/practiceTactics` | Get all practice tactics of a specific project |

#### Practice
| HTTP Method | Endpoint | Description |
| :---: | --- | --- |
| `GET` | `api/v1/pages/{id}/getPracticeList` | Get flashcard practice list from a page and below |

## Software Structure Diagrams

---

## Architure Patterns

---

## Glossary

---

### Other repositories used and required to work:
- https://github.com/Dariusz-L/Corelibs.Basic
- https://github.com/Dariusz-L/Corelibs.BlazorShared
- https://github.com/Dariusz-L/Corelibs.BlazorComponents
- https://github.com/Dariusz-L/Corelibs.AspNetApi
- https://github.com/Dariusz-L/Corelibs.BlazorWebAssembly
- https://github.com/Dariusz-L/Corelibs.MauiMsalAuth
- https://github.com/Dariusz-L/Corelibs.MongoDB
