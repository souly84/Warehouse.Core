# Warehouse.Core

[![Warehouse.Core package in Warehouse feed in Azure Artifacts](https://souleymen.feeds.visualstudio.com/5e7ba3a8-de58-4498-aed2-a23e91696074/_apis/public/Packaging/Feeds/6754a99f-fc1f-4540-be65-d313fae61071/Packages/2b86139a-c6c0-4de8-890c-5f8541a7d552/Badge)](https://souleymen.visualstudio.com/Warehouse/_packaging?_a=package&feed=6754a99f-fc1f-4540-be65-d313fae61071&package=2b86139a-c6c0-4de8-890c-5f8541a7d552&preferRelease=true)
[![Hits-of-Code](https://hitsofcode.com/github/souly84/Warehouse.Core?branch=main)](https://hitsofcode.com/github/souly84/Warehouse.Core?branch=main/view)
[![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=souly84_InventoryOperations&metric=ncloc)](https://sonarcloud.io/dashboard?id=souly84_InventoryOperations)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=souly84_InventoryOperations&metric=coverage)](https://sonarcloud.io/dashboard?id=souly84_InventoryOperations)

The repository contains domain entities and contracts that help to manage goods in warehouse storage. The root domain entity is [ICompany](https://github.com/souly84/Warehouse.Core/blob/main/src/(Company)/ICompany.cs) provides an access to all business entities. The repository represents Entities project in [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html) approach.

## Plugins

Warehouse.Core is currently extended with the particular plugins that implement contracts declared in current repository. Instructions on how to use them are in own repositories linked below.

| Repository | Version |
| ------ | ------ |
| [EbSoft.SDK](https://github.com/souly84/EbSoft.Warehouse.SDK) | [![EbSoft.Warehouse.SDK package in Warehouse feed in Azure Artifacts](https://souleymen.feeds.visualstudio.com/5e7ba3a8-de58-4498-aed2-a23e91696074/_apis/public/Packaging/Feeds/6754a99f-fc1f-4540-be65-d313fae61071/Packages/3e29a369-faf2-402c-b043-6f2deb71a29f/Badge)](https://souleymen.visualstudio.com/Warehouse/_packaging?_a=package&feed=6754a99f-fc1f-4540-be65-d313fae61071&package=3e29a369-faf2-402c-b043-6f2deb71a29f&preferRelease=true) |
| [Oddo.SDK](https://github.com/souly84/Odoo.Warehouse.SDK) | [![Odoo.Warehouse.SDK package in Warehouse feed in Azure Artifacts](https://souleymen.feeds.visualstudio.com/5e7ba3a8-de58-4498-aed2-a23e91696074/_apis/public/Packaging/Feeds/6754a99f-fc1f-4540-be65-d313fae61071/Packages/c19438d5-fdc4-45b8-9c95-c60edf85c208/Badge)](https://souleymen.visualstudio.com/Warehouse/_packaging?_a=package&feed=6754a99f-fc1f-4540-be65-d313fae61071&package=c19438d5-fdc4-45b8-9c95-c60edf85c208&preferRelease=true) |
| [Warehouse.Mobile](https://github.com/souly84/Warehouse.Mobile) | None

## Company

The diagram below shows all the entities that company interface provides access to. At the moment only 2 entities were described in details:

- Warehouse
- Supplier

The main goal is to provide some kind of root node to access the SDK features. The developers should implement [ICompany](https://github.com/souly84/Warehouse.Core/blob/main/src/(Company)/ICompany.cs) contract and as a result it forces the developers to implement all the dependencies that are important for SDK to work properly.

![Company UML diagram](/docs/Company.uml.svg?raw=true "Classes dependencies diagram")

## Warehouse

The diagram below shows all the entities that warehouse interface provides access to. The main responsibility is to give an access to collection of goods in the storage. There are a couple of major features that warehouse employee is processing:

- Order goods preparation for delivery (collecting goods and move then to PutAway storage the day before delivery).
- Goods movement inside the warehouse between the storages.
- Assigning the location to the goods that were validated during the reception validation process.

![Warehouse UML diagram](/docs/warehouse.uml.svg?raw=true "Classes dependencies diagram")

## Storage

 The warehouse is a sequence of locations (the shelfs marked with unique identifiers). There are different storage types usually used in the warehouse:

- Put away storage
- Race storage
- Reserve storage

Each storage contains the collecrtion of goods that were assigned to it.

## Supplier

The diagram below shows all the entities that supplier interface provides access to:

- The main responsibility is to give an access to collection of receptions that need to be validated by warehouse employee during delivery validation process.
- Usually warehouse employee uses barcode scanner to confirm  goods that are part of the reception. Once all the goods were confirmed a validation request is sent to the server to finish the reception processing.
- During the reception confirmation process there is unexpected good can be part of reception delivery. The application should be able to handle such case properly. To do so [ReceptionWithUnknownGoods](https://github.com/souly84/Warehouse.Core/blob/docs-updates/src/Warehouse.Core/(Receptions)/(Goods)/ReceptionWithUnkownGoods.cs) should be used to handle such case.
- During the reception confirmation process some extra good can be detected as a part of reception delivery. The application should be able to handle such case properly. [ReceptionWithExtraConfirmedGoods](https://github.com/souly84/Warehouse.Core/blob/docs-updates/src/Warehouse.Core/(Receptions)/(Goods)/ReceptionWithExtraConfirmedGoods.cs) should be used to handle such case.

![Supplier UML diagram](/docs/Supplier.uml.svg?raw=true "Classes dependencies diagram")

## Status

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=souly84_InventoryOperations&metric=alert_status)](https://sonarcloud.io/dashboard?id=souly84_InventoryOperations)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=souly84_InventoryOperations&metric=sqale_rating)](https://sonarcloud.io/dashboard?id=souly84_InventoryOperations)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=souly84_InventoryOperations&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=souly84_InventoryOperations)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=souly84_InventoryOperations&metric=security_rating)](https://sonarcloud.io/dashboard?id=souly84_InventoryOperations)

## Technical Depth

[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=souly84_InventoryOperations&metric=bugs)](https://sonarcloud.io/dashboard?id=souly84_InventoryOperations)
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=souly84_InventoryOperations&metric=code_smells)](https://sonarcloud.io/dashboard?id=souly84_InventoryOperations)
[![Duplicated Lines (%)](https://sonarcloud.io/api/project_badges/measure?project=souly84_InventoryOperations&metric=duplicated_lines_density)](https://sonarcloud.io/dashboard?id=souly84_InventoryOperations)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=souly84_InventoryOperations&metric=vulnerabilities)](https://sonarcloud.io/dashboard?id=souly84_InventoryOperations)
[![Technical Debt](https://sonarcloud.io/api/project_badges/measure?project=souly84_InventoryOperations&metric=sqale_index)](https://sonarcloud.io/dashboard?id=souly84_InventoryOperations)
