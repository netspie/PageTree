# PageTree

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;A tool for knowledge organization in a form of pages structured in tree-like layout with highly interconnected and internavigatable elements for automatic, manually configured or based on spaced-repetition algorithm flashcards generation.

Meant to provide a way for organizing notes or any knowledge database in a nice way, and be able to generate list of flashcards for any specific kind of data in there.
Each piece of data is a page itself.

My initial idea is to create a helpful tool for learning japanese language specifically, but I believe it can be applied for any domain out there. The real challenge is to structure data in well-thought manner, so to make the exploration/navigation pleasant.

Available sources of learning:
- dictionaries - just words, dumb descriptions, little to no context
- mobile apps
- paid courses / platforms

Japanese Language Learning Materials Structure:
- Material Origins / Authors / Material Creators
    - Author 1
    
- Scripts
    -

Not just learning tool

Would be cool for a learning content creator to provide a ready material

Japanese Learning Materials Challenges:
- provide 
- mobile apps
- paid courses / platforms

Here is the .. that emerged from the mentioned problems:
"Page" is the most fundamental concept here and has following properties:
- it can be any piece of data, preferably very small, easily memorizable
- every page can have descendants (children, or so called "properties") you can navigate to
- children can be of various types
    - subpages - 
    - links - related data to
    - queries
    - metadata
- every page is really just a list
- every page can be highly customizable, both in layout and styling
- every page list allows for manipulation - sort, filter, custom order

Once again in bulletpoints:
- structurized knowledge base of any domain - initially created for japanese language learning with potential to expand for other subjects
- automatically generated flashcards - depending on what level of the pages / knowledge tree you will navigate to and want the flashcard start to be generated from
- spaced repetition algorithm, proposing already searched or learned materials - all configurable
- target plaforms - web and mobile
- working online and offline

## Problem Description

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Preparing notes requires time and effort, which could be spend on just learning itself. Of course it does not have to be absolutely excluded from a learning process, since it's a way to learn by itself too. Although, why to waste time just to convert a knowledge from one type of media to the other, when it's just can be ready to consume.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Browsing, learning from the same notes either written down on a paper or a computer every day becomes so boring after a time.. It brings routine. It slowly makes you start to procrastinate again.

## Tech

C# 10, .NET 6-7, EF Core/MS SQL/.json, Blazor WASM, .NET MAUI, Lucene.NET, Mediator, AutoMapper, WebApi

### Architecture
- Client-Server
- Clean Architecture
- CQRS

## Deliverables
The app prototype is available on https://japanesearcana.com
It is hosted on sharkasp.com

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
