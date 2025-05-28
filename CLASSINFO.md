# Описание классов и их функциональности

Этот документ содержит описание всех классов и их методов.

---
## Оглавление 
### Основные классы
1. [AttackRepository](#attackrepository) - Управление репозиторием ATT&CK
2. [Collection](#collection) - Управление коллекциями объектов STIX
### Вспомогательные классы
3. [TaxiiApi](#taxiiapi) - Взаимодействие с TAXII API
4. [StixDataConverter](#stixdataconverter) - Десериализация объектов STIX
### Классы сущностей
5. [StixObject](#stixobject) - Базовый класс для всех объектов STIX
6. [StixData](#stixdata) - Контейнер для всех объектов ATT&CK
7. [StixCollection](#stixcollection) - Коллекция объектов ATT&CK
8. [StixMatrix](#stixmatrix) - Матрица тактик ATT&CK
9. [StixTactic](#stixtactic) - Тактики ATT&CK
10. [StixAttackPattern](#stixattackpattern) - Техники атак
11. [StixCourseOfAction](#stixcourseofaction) - Рекомендации по защите
12. [StixIntrusionSet](#stixintrusionset) - Группы злоумышленников
13. [StixCampaign](#stixcampaign) - Кампании атак
14. [StixMalware](#stixmalware) - Вредоносное ПО
15. [StixTool](#stixtool) - Инструменты атак
16. [StixDataSource](#stixdatasource) - Источники данных
17. [StixDataComponent](#stixdatacomponent) - Компоненты данных
18. [StixAsset](#stixasset) - Целевые активы
19. [StixIdentity](#stixidentity) - Идентификационные данные
20. [StixRelationship](#stixrelationship) - Связи между объектами
---
## Класс `AttackRepository` <a name="attackrepository"></a>

**Описание:**
Класс для управления репозиторием атак, взаимодействующий с TAXII API для получения данных STIX. Наследует функциональность `TaxiiApi` и предоставляет методы для создания и управления коллекциями объектов STIX.

### Общая информация
| Характеристика | Значение |
|---------------|----------|
| Пространство имен | `MitreAttackParser` |
| Наследование | `TaxiiApi` |
| Назначение | Управление репозиторием атак через TAXII API |

### Свойства класса
| Свойство | Тип | Описание |
|----------|-----|-----------|
| `ServerAddress` | `string` | Адрес сервера TAXII API. |
| `PooledConnectionLifetime` | `double` | Время жизни соединения в пуле (в минутах). |
| `PooledConnectionIdleTimeout` | `double` | Время простоя соединения в пуле (в минутах). |
| `Timeout` | `double` | Таймаут для операций HTTP-запросов (в минутах). |
| `EnterpriseCollection` | `Collection` | Коллекция объектов STIX для Enterprise. |
| `ICSCollection` | `Collection` | Коллекция объектов STIX для Industrial Control Systems (ICS). |
| `MobileCollection` | `Collection` | Коллекция объектов STIX для мобильных устройств. |

### Конструктор

#### `AttackRepository(string serverAddress = "https://attack-taxii.mitre.org/api/v21", double pooledConnectionLifetime = 5, double pooledConnectionIdleTimeout = 2, double timeout = 5)`
Инициализирует новый экземпляр класса `AttackRepository`.

**Параметры:**
- `serverAddress` - Адрес сервера TAXII API (по умолчанию: "https://attack-taxii.mitre.org/api/v21").
> **Примечание:** Вы можете использовать локальный TAXII API, но вам необходимо убедится, что его конечный точки соответствуют работе класса [TaxiiApi](#taxiiapi)
- `pooledConnectionLifetime` - Время жизни соединения в пуле (в минутах, по умолчанию: 5).
- `pooledConnectionIdleTimeout` - Время простоя соединения в пуле (в минутах, по умолчанию: 2).
- `timeout` - Таймаут для операций HTTP-запросов (в минутах, по умолчанию: 5).

**Логика работы:**
1. Устанавливает значения для `ServerAddress`, `PooledConnectionLifetime`, `PooledConnectionIdleTimeout` и `Timeout`.
2. Инициализирует `HttpClient` с настройками таймаутов и времени жизни соединений, используя предоставленные значения.
3. Устанавливает опции сериализации JSON с учетом регистра и добавлением конвертера `StixDataConverter`.

### Методы

#### `CreateAsync() : Task<bool>`
Создает коллекции, загружая все объекты из указанных URL.

**Возвращает:**
- `true`, если все коллекции успешно созданы

**Логика работы:**
1. Получает список идентификаторов коллекций с помощью метода `GetCollectionId`.
2. Инициализирует коллекции `EnterpriseCollection`, `ICSCollection` и `MobileCollection` с соответствующими URL.
3. Вызывает метод `CreateAsync` для каждой коллекции.
4. Возвращает `true` в случае успешного выполнения для всех коллекций.

---

## Класс `Collection` <a name="collection"></a>

**Описание:**  
Класс для работы с коллекциями STIX через TAXII API. Наследует функциональность `TaxiiApi` и предоставляет методы для создания и управления коллекциями объектов STIX.

### Общая информация
 | Характеристика | Значение |
 |---------------|----------|
 | Пространство имен | `MitreAttackParser.Models` |
 | Наследование | `TaxiiApi` |
 | Назначение | Управление коллекциями объектов STIX |

### Свойства класса
 | Свойство | Тип | Описание |
 |----------|-----|-----------|
 | `AboutCollection` | `StixCollection` | Возвращает информацию о текущей коллекции. |
 | `Techniques` | `List<StixAttackPattern>` | Список объектов техник (Attack Patterns). |
 | `Campaigns` | `List<StixCampaign>` | Список объектов кампаний (Campaigns). |
 | `CourseOfActions` | `List<StixCourseOfAction>` | Список объектов курсов действий (Course of Actions). |
 | `Identities` | `List<StixIdentity>` | Список объектов идентификаторов (Identities). |
 | `IntrusionSets` | `List<StixIntrusionSet>` | Список объектов наборов вторжений (Intrusion Sets). |
 | `Malwares` | `List<StixMalware>` | Список объектов вредоносных программ (Malwares). |
 | `Relationships` | `List<StixRelationship>` | Список объектов отношений (Relationships). |
 | `Tools` | `List<StixTool>` | Список объектов инструментов (Tools). |
 | `DataComponents` | `List<StixDataComponent>` | Список объектов компонентов данных (Data Components). |
 | `DataSources` | `List<StixDataSource>` | Список объектов источников данных (Data Sources). |
 | `Matrices` | `List<StixMatrix>` | Список объектов матриц (Matrices). |
 | `Tactics` | `List<StixTactic>` | Список объектов тактик (Tactics). |
 | `Assets` | `List<StixAsset>?` | Список объектов активов (Assets). |

### Конструктор

#### `Collection(HttpClient httpClient, JsonSerializerOptions jsonSerializerOptions, string url)`
Инициализирует новый экземпляр класса `Collection`.

**Параметры:**
- `httpClient` - Клиент для выполнения HTTP-запросов
- `jsonSerializerOptions` - Опции сериализации JSON
- `url` - URL для запроса к API

### Методы

#### `CreateAsync() : Task<bool>`
Создает коллекцию, загружая все объекты из указанного URL.

**Возвращает:**
- `true`, если коллекция успешно создана

**Логика работы:**
1. Получает все объекты коллекции с помощью метода `GetAllCollectionObjects`.
2. Заполняет свойства класса соответствующими объектами.
3. Возвращает `true` в случае успешного выполнения.

---

## Класс `TaxiiApi` <a name="taxiiapi"></a>

**Описание:**  
Реализация интерфейса `ITaxiiApi` для взаимодействия с TAXII API. Обеспечивает получение информации о коллекциях и объектах STIX через HTTP-запросы.

### Общая информация
 | Характеристика | Значение |
 |---------------|----------|
 | Пространство имен | `MitreAttackParser.Helpers` |
 | Наследование | `ITaxiiApi` |
 | Назначение | Взаимодействие с TAXII API для получения данных STIX |

### Внутренние структуры

#### `StixCollectionInfo`
Структура для хранения информации о коллекции STIX.

**Поля:**
- `Id` - Идентификатор коллекции
- `Title` - Название коллекции
- `Description` - Описание коллекции
- `CanRead` - Флаг возможности чтения
- `CanWrite` - Флаг возможности записи
- `MediaTypes` - Список поддерживаемых медиа типов

### Методы

#### `GetCollectionId(HttpClient httpClient, JsonSerializerOptions jsonSerializerOptions, string url) : Task<List<string>>`
Получает список идентификаторов коллекций из TAXII API.
 > **Примечание:** Конечная точка: {адрес сервера}/collections

**Параметры:**
- `httpClient` - Клиент для выполнения HTTP-запросов
- `jsonSerializerOptions` - Опции сериализации JSON
- `url` - URL для запроса к API

**Возвращает:**
- Список идентификаторов коллекций

**Логика работы:**
1. Выполняет HTTP-запрос к указанному URL.
2. Десериализует ответ в словарь, содержащий список `StixCollectionInfo`.
3. Извлекает и возвращает идентификаторы коллекций.

#### `GetAllCollectionObjects(HttpClient httpClient, JsonSerializerOptions jsonSerializerOptions, string url) : Task<StixData>`
Получает все объекты из указанной коллекции.
 > **Примечание:** Конечная точка: {адрес сервера}/collections/{id коллекции}/objects

**Параметры:**
- `httpClient` - Клиент для выполнения HTTP-запросов
- `jsonSerializerOptions` - Опции сериализации JSON
- `url` - URL для запроса к API

**Возвращает:**
- Объект `StixData`, содержащий все объекты коллекции

**Логика работы:**
1. Выполняет HTTP-запрос к указанному URL.
2. Десериализует ответ в объект `StixData`.
3. Возвращает объект `StixData`.

#### `SendResponseAsync(HttpClient httpClient, string url) : Task<string>`
Выполняет HTTP-запрос и возвращает ответ в виде строки.

**Параметры:**
- `httpClient` - Клиент для выполнения HTTP-запросов
- `url` - URL для запроса к API

**Возвращает:**
- Ответ от сервера в виде строки

**Логика работы:**
1. Создает HTTP-запрос с заголовком `Accept: application/taxii+json; version=2.1`.
2. Отправляет запрос и проверяет успешность выполнения.
3. Возвращает ответ в виде строки.

---
## Класс `StixDataConverter`<a name="stixdataconverter"></a>

**Описание:**  
Кастомный JSON-конвертер для десериализации объектов MITRE ATT&CK в формате STIX. Обрабатывает сложную структуру данных ATT&CK, распределяя объекты по соответствующим коллекциям в классе `StixData`.

### Общая информация
| Характеристика | Значение |
|---------------|----------|
| Пространство имен | `MitreAttackParser.Helpers` |
| Наследование | `JsonConverter<StixData>` |
| Назначение | Десериализация комплексных STIX-данных ATT&CK |

### Методы

#### `Read(ref Utf8JsonReader, Type, JsonSerializerOptions) : StixData`
Десериализует JSON в объект `StixData`, распределяя объекты по соответствующим коллекциям.

**Параметры:**
- `reader` - JSON reader для чтения данных
- `typeToConvert` - Тип для конвертации (StixData)
- `options` - Опции сериализации

**Возвращает:**
- Заполненный объект `StixData`

**Логика обработки:**
1. Анализирует массив `objects` в JSON
2. Для каждого объекта определяет тип через поле `type`
3. Десериализует объект в соответствующий класс
4. Добавляет в нужную коллекцию в `StixData`

**Обрабатываемые типы:**
```csharp
"x-mitre-collection"       => StixCollection
"attack-pattern"           => StixAttackPattern
"campaign"                 => StixCampaign
"course-of-action"         => StixCourseOfAction
"identity"                 => StixIdentity
"intrusion-set"            => StixIntrusionSet
"malware"                  => StixMalware
"relationship"             => StixRelationship
"tool"                     => StixTool
"x-mitre-data-component"   => StixDataComponent
"x-mitre-data-source"      => StixDataSource
"x-mitre-matrix"           => StixMatrix
"x-mitre-tactic"           => StixTactic
"x-mitre-asset"            => StixAsset
```
#### `Write(Utf8JsonWriter writer, StixData value, JsonSerializerOptions options)`
Сериализует объект `StixData` в JSON-формат, сохраняя оригинальную структуру данных с массивом `objects`.

**Параметры:**
- `writer` - JSON writer для для записи
- `value` - Объект данных для сериализации (StixData)
- `options` - Опции сериализации

**Возвращает**  
`void` (отсутствует)

**Логика работы:**

1. **Проверка входных параметров:**
   - Выбрасывает `ArgumentNullException`, если `writer` равен null
   - Записывает `null` если `value` равен null

2. **Структура вывода:**
   ```json
   {
     "objects": [
       { /* первый объект */ },
       { /* второй объект */ }
     ]
   }
   ```
---
## Класс `StixObject` <a name="stixobject"></a>

**Описание:**  
Абстрактный базовый класс для всех объектов STIX в модели MITRE ATT&CK. Определяет общие свойства и поведение для всех сущностей.

### Общая информация
| Характеристика       | Значение                          |
|----------------------|-----------------------------------|
| Пространство имен    | `MitreAttackParser.Entities`  |
| Модификатор          | `abstract`                        |
| JSON-атрибуты        | `[JsonPolymorphic]`, `[JsonDerivedType]` |
| Назначение           | Базовый класс для STIX-объектов   |

### Свойства класса

| Свойство | Тип | JSON-атрибут | По умолчанию | Описание |
|----------|-----|--------------|--------------|-----------|
| `Id`                      | `string`                | `[JsonPropertyName("id")]`            | -                               | Уникальный идентификатор объекта STIX |
| `Type`                    | `string`                | `[JsonPropertyName("type")]`<br>`[JsonIgnore]` | -                     | Абстрактное свойство типа объекта |
| `SpecVersion`             | `string`                | `[JsonPropertyName("spec_version")]`  | `"2.1"`                         | Версия спецификации STIX |
| `Created`                 | `DateTime`              | `[JsonPropertyName("created")]`       | -                               | Дата и время создания объекта |
| `Modified`                | `DateTime`              | `[JsonPropertyName("modified")]`      | -                               | Дата и время последнего изменения |
| `CreatedByRef`            | `string`                | `[JsonPropertyName("created_by_ref")]`| -                               | Ссылка на создателя объекта |
| `ObjectMarkingRefs`       | `List<string>`          | `[JsonPropertyName("object_marking_refs")]` | `new List<string>()`    | Список ссылок на маркировки |
| `MitreModifiedByRef`      | `string`                | `[JsonPropertyName("x_mitre_modified_by_ref")]` | -                | Ссылка на изменившего объект |
| `ExternalReferences`      | `List<MitreExternalReference>` | `[JsonPropertyName("external_references")]` | `new List<MitreExternalReference>()` | Внешние ссылки на связанные ресурсы |
| `MitreAttackSpecVersion`  | `string`                | `[JsonPropertyName("x_mitre_attack_spec_version")]` | `"3.2.0"`              | Версия спецификации MITRE ATT&CK |
| `MitreDeprecated`         | `bool`                  | `[JsonPropertyName("x_mitre_deprecated")]` | `false`                   | Флаг устаревания объекта |
| `MitreVersion`            | `string`                | `[JsonPropertyName("x_mitre_version")]` | -                          | Версия объекта в MITRE |

### Вложенный класс `MitreExternalReference`
**Описание:**  
Представляет внешнюю ссылку на дополнительные ресурсы, связанные с объектом STIX. Используется для хранения метаданных и перекрестных ссылок между различными источниками данных.
| Свойство | Тип | JSON-атрибут | По умолчанию | Описание |
|----------|-----|--------------|--------------|-----------|
| `SourceName`      | `string`  | `[JsonPropertyName("source_name")]` | - |Наименование источника |
| `ExternalId`      | `string`  | `[JsonPropertyName("external_id")]` | -| Внешний идентификатор |
| `Url`            | `string`  | `[JsonPropertyName("url")]`         | -| URL ссылки |
| `Description`    | `string`  | `[JsonPropertyName("description")]` | -| Описание ссылки |

---

## Класс `StixData` <a name="stixdata"></a>

**Описание:**  
Контейнерный класс для хранения полного набора данных MITRE ATT&CK в STIX-формате. Служит корневым объектом для сериализации/десериализации всей таксономии ATT&CK.

### Общая информация
| Характеристика | Значение |
|---------------|----------|
| Пространство имен | `MitreAttackParser.Entities` |
| Назначение | Хранение и организация всех объектов MITRE ATT&CK |

### Свойства класса
| Свойство | Тип | Описание | Особенности |
|----------|-----|----------|-------------|
| `Collection` | `MitreCollection` | Метаинформация о коллекции | Основной контейнер |
| `AttackPatterns` | `List<MitreAttackPattern>` | Техники атак | Базовые элементы ATT&CK |
| `Campaigns` | `List<MitreCampaign>` | Кампании атак | Группировка по операциям |
| `CourseOfActions` | `List<MitreCourseOfAction>` | Контрмеры | Рекомендации по защите |
| `Identities` | `List<MitreIdentity>` | Идентификаторы | Организации/цели |
| `IntrusionSets` | `List<MitreIntrusionSet>` | Группы атакующих | APT-группы |
| `Malwares` | `List<MitreMalware>` | Вредоносное ПО | Инструменты атак |
| `Relationships` | `List<MitreRelationship>` | Связи между объектами | Граф отношений |
| `Tools` | `List<MitreTool>` | Легитимные инструменты | Используемые злоумышленниками |
| `DataComponents` | `List<MitreDataComponent>` | Компоненты данных | Для обнаружения |
| `DataSources` | `List<MitreDataSource>` | Источники данных | Телеметрия |
| `Matrices` | `List<MitreMatrix>` | Матрицы тактик | Организация тактик |
| `Tactics` | `List<MitreTactic>` | Тактики ATT&CK | Цели атакующих |
| `Assets` | `List<MitreAsset>?` | Активы (опционально) | Целевые ресурсы |
| `AssetsList` | `List<MitreAsset>` | Геттер для активов | Всегда возвращает список |

---

## Класс `StixCollection` <a name="stixcollection"></a>

**Описание:**  
Представляет коллекцию объектов в модели данных MITRE ATT&CK. Содержит метаинформацию о наборе взаимосвязанных объектов ATT&CK.

### Общая информация
| Характеристика | Значение |
|---------------|----------|
| Пространство имен | `MitreAttackParser.Entities` |
| Наследование | `MitreStixObject` |
| Тип STIX | `x-mitre-collection` |
| Назначение | Группировка объектов ATT&CK |

### Свойства класса
| Свойство | Тип | JSON-атрибут | По умолчанию | Описание |
|----------|-----|--------------|--------------|-----------|
| `Type` | `string` | `[JsonPropertyName("type")]` | `"x-mitre-collection"` | Тип объекта (переопределен) |
| `Name` | `string` | `[JsonPropertyName("name")]` | - | Наименование коллекции |
| `Description` | `string` | `[JsonPropertyName("description")]` | - | Детальное описание |
| `Contents` | `List<MitreContent>` | `[JsonPropertyName("x_mitre_contents")]` | `new List<MitreContent>()` | Содержимое коллекции |
| `Domains` | `List<string>` | `[JsonPropertyName("x_mitre_domains")]` | `new List<string>()` | Домены ATT&CK |

### Вложенный класс `MitreContent`

**Описание:**  
Представляет элемент содержимого коллекции, связывая ссылку на объект с метаданными о его модификации.

### Свойства класса
| Свойство | Тип | JSON-атрибут | По умолчанию | Описание |
|----------|-----|--------------|--------------|-----------|
| `ObjectRef` | `string` | `[JsonPropertyName("object_ref")]` | - | STIX ID связанного объекта |
| `ObjectModified` | `DateTime` | `[JsonPropertyName("object_modified")]` | - | Когда объект последний раз изменялся |

---

## Класс `StixMatrix` <a name="stixmatrix"></a>

**Описание:**  
Представляет матрицу тактик в модели данных MITRE ATT&CK. Служит для организации тактик в логические группы.

### Общая информация
| Характеристика | Значение |
|---------------|----------|
| Пространство имен | `MitreAttackParser.Entities` |
| Наследование | `MitreStixObject` |
| Тип STIX | `x-mitre-matrix` |
| Назначение | Организация тактик ATT&CK в матрицы |

### Свойства класса
| Свойство | Тип | JSON-атрибут | По умолчанию | Описание |
|----------|-----|--------------|--------------|-----------|
| `Type` | `string` | `[JsonPropertyName("type")]` | `"x-mitre-matrix"` | Тип объекта (переопределен) |
| `Name` | `string` | `[JsonPropertyName("name")]` | - | Наименование матрицы |
| `Description` | `string` | `[JsonPropertyName("description")]` | - | Детальное описание матрицы |
| `TacticRefs` | `List<string>` | `[JsonPropertyName("tactic_refs")]` | `new List<string>()` | Ссылки на тактики |

---

## Класс `StixTactic` <a name="stixtactic"></a>

**Описание:**  
Представляет тактику в модели данных MITRE ATT&CK. Тактики описывают цели атакующих на высоком уровне.

### Общая информация
| Характеристика | Значение |
|---------------|----------|
| Пространство имен | `MitreAttackParser.Entities` |
| Наследование | `MitreStixObject` |
| Тип STIX | `x-mitre-tactic` |
| Назначение | Описание тактических целей атакующих |

### Свойства класса
| Свойство | Тип | JSON-атрибут | По умолчанию | Описание |
|----------|-----|--------------|--------------|-----------|
| `Type` | `string` | `[JsonPropertyName("type")]` | `"x-mitre-tactic"` | Тип объекта (переопределен) |
| `Name` | `string` | `[JsonPropertyName("name")]` | - | Полное название тактики |
| `Description` | `string` | `[JsonPropertyName("description")]` | - | Подробное описание тактики |
| `MitreShortname` | `string` | `[JsonPropertyName("x_mitre_shortname")]` | - | Короткое имя тактики (ID) |

---

## Класс `StixAttackPattern` <a name="stixattackpattern"></a>

**Описание:**  
Представляет технику атаки (attack pattern) в модели MITRE ATT&CK. Описывает конкретные методы, используемые злоумышленниками для достижения тактических целей.

### Общая информация
| Характеристика | Значение |
|---------------|----------|
| Пространство имен | `MitreAttackParser.Entities` |
| Наследование | `MitreStixObject` |
| Тип STIX | `attack-pattern` |
| Назначение | Описание конкретных техник атак |

### Свойства класса
| Свойство | Тип | JSON-атрибут | По умолчанию | Описание |
|----------|-----|--------------|--------------|-----------|
| `Type` | `string` | `[JsonPropertyName("type")]` | `"attack-pattern"` | Тип объекта (переопределен) |
| `Name` | `string` | `[JsonPropertyName("name")]` | - | Название техники |
| `Description` | `string` | `[JsonPropertyName("description")]` | - | Подробное описание |
| `KillChainPhases` | `List<MitreKillChainPhase>` | `[JsonPropertyName("kill_chain_phases")]` | `new List<MitreKillChainPhase>()` | Фазы kill chain |
| `MitreDetection` | `string` | `[JsonPropertyName("x_mitre_detection")]` | - | Рекомендации по обнаружению |
| `MitrePlatforms` | `List<string>` | `[JsonPropertyName("x_mitre_platforms")]` | `new List<string>()` | Целевые платформы |
| `MitreIsSubtechnique` | `bool` | `[JsonPropertyName("x_mitre_is_subtechnique")]` | `false` | Флаг подтехники |
| `MitreDataSources` | `List<string>` | `[JsonPropertyName("x_mitre_data_sources")]` | `new List<string>()` | Источники данных |

### Вложенный класс: `MitreKillChainPhase` <a name="kill-chain-phase"></a>

**Описание:**  
Определяет фазу kill chain, к которой относится техника.

#### Свойства
| Свойство | Тип | JSON-атрибут | По умолчанию | Описание |
|----------|-----|--------------|--------------|-----------|
| `KillChainName` | `string` | `[JsonPropertyName("kill_chain_name")]` | - | Название kill chain |
| `PhaseName` | `string` | `[JsonPropertyName("phase_name")]` | - | Название фазы |

---

## Класс `StixIntrusionSet` <a name="stixintrusionset"></a>

**Описание:**  
Представляет группу злоумышленников (Intrusion Set) в модели MITRE ATT&CK. Описывает устойчивые группы, которые проводят целевые атаки.

### Общая информация
| Характеристика | Значение |
|---------------|----------|
| Пространство имен | `MitreAttackParser.Entities` |
| Наследование | `MitreStixObject` |
| Тип STIX | `intrusion-set` |
| Назначение | Описание групп злоумышленников и их активности |

### Свойства класса
| Свойство | Тип | JSON-атрибут | По умолчанию | Описание |
|----------|-----|--------------|--------------|-----------|
| `Type` | `string` | `[JsonPropertyName("type")]` | `"intrusion-set"` | Тип объекта (переопределен) |
| `Name` | `string` | `[JsonPropertyName("name")]` | - | Официальное название группы |
| `Description` | `string` | `[JsonPropertyName("description")]` | - | Подробное описание группы |
| `Aliases` | `List<string>` | `[JsonPropertyName("aliases")]` | `new List<string>()` | Альтернативные названия |
| `MitreContributors` | `List<string>` | `[JsonPropertyName("x_mitre_contributors")]` | `new List<string>()` | Авторы информации о группе |

---

## Класс `StixTool` <a name="stixtool"></a>

**Описание:**  
Представляет легитимные инструменты (Tools) в модели MITRE ATT&CK, которые могут быть использованы злоумышленниками в malicious целях. Включает системные утилиты, административные инструменты и другое ПО двойного назначения.

### Общая информация
| Характеристика | Значение |
|---------------|----------|
| Пространство имен | `MitreAttackParser.Entities` |
| Наследование | `MitreStixObject` |
| Тип STIX | `tool` |
| Назначение | Описание инструментов, используемых при атаках |

### Свойства класса
| Свойство | Тип | JSON-атрибут | По умолчанию | Описание |
|----------|-----|--------------|--------------|-----------|
| `Type` | `string` | `[JsonPropertyName("type")]` | `"tool"` | Тип объекта (переопределен) |
| `Name` | `string` | `[JsonPropertyName("name")]` | - | Официальное название инструмента |
| `Description` | `string` | `[JsonPropertyName("description")]` | - | Описание функциональности и использования |
| `MitreAliases` | `List<string>` | `[JsonPropertyName("x_mitre_aliases")]` | `new List<string>()` | Альтернативные названия и версии |

---

## Класс `StixCampaign` <a name="stixcampaign"></a>

**Описание:**  
Представляет кампанию (Campaign) в модели MITRE ATT&CK. Описывает серию связанных атак, проводимых одной или несколькими группами злоумышленников в течение определенного периода времени.

### Общая информация
| Характеристика | Значение |
|---------------|----------|
| Пространство имен | `MitreAttackParser.Entities` |
| Наследование | `MitreStixObject` |
| Тип STIX | `campaign` |
| Назначение | Описание серий связанных атак и операций |

### Свойства класса
| Свойство | Тип | JSON-атрибут | По умолчанию | Описание |
|----------|-----|--------------|--------------|-----------|
| `Type` | `string` | `[JsonPropertyName("type")]` | `"campaign"` | Тип объекта (переопределен) |
| `Name` | `string` | `[JsonPropertyName("name")]` | - | Название кампании |
| `Description` | `string` | `[JsonPropertyName("description")]` | - | Подробное описание кампании |
| `Aliases` | `List<string>` | `[JsonPropertyName("aliases")]` | `new List<string>()` | Альтернативные названия |
| `FirstSeen` | `DateTime` | `[JsonPropertyName("first_seen")]` | - | Дата первого обнаружения |
| `LastSeen` | `DateTime` | `[JsonPropertyName("last_seen")]` | - | Дата последнего обнаружения |
| `MitreFirstSeenCitation` | `string` | `[JsonPropertyName("x_mitre_first_seen_citation")]` | - | Источник информации о первом обнаружении |
| `MitreLastSeenCitation` | `string` | `[JsonPropertyName("x_mitre_last_seen_citation")]` | - | Источник информации о последнем обнаружении |

---

## Класс `StixCourseOfAction` <a name="stixcourseofaction"></a>

**Описание:**  
Представляет рекомендации по противодействию (Course of Action) в модели MITRE ATT&CK. Содержит методы смягчения и защиты от различных техник атак.

### Общая информация
| Характеристика | Значение |
|---------------|----------|
| Пространство имен | `MitreAttackParser.Entities` |
| Наследование | `MitreStixObject` |
| Тип STIX | `course-of-action` |
| Назначение | Рекомендации по защите и противодействию атакам |

### Свойства класса
| Свойство | Тип | JSON-атрибут | По умолчанию | Описание |
|----------|-----|--------------|--------------|-----------|
| `Type` | `string` | `[JsonPropertyName("type")]` | `"course-of-action"` | Тип объекта (переопределен) |
| `Name` | `string` | `[JsonPropertyName("name")]` | - | Название рекомендации |
| `Description` | `string` | `[JsonPropertyName("description")]` | - | Подробное описание рекомендации |

---

## Класс `StixIdentity` <a name="stixidentity"></a>

**Описание:**  
Представляет идентификационные данные (Identity) в модели MITRE ATT&CK. Используется для описания организаций, групп или других субъектов, которые могут быть целями атак или участниками киберопераций.

### Общая информация
| Характеристика | Значение |
|---------------|----------|
| Пространство имен | `MitreAttackParser.Entities` |
| Наследование | `MitreStixObject` |
| Тип STIX | `identity` |
| Назначение | Описание организаций и субъектов в контексте кибербезопасности |

### Свойства класса
| Свойство | Тип | JSON-атрибут | По умолчанию | Описание |
|----------|-----|--------------|--------------|-----------|
| `Type` | `string` | `[JsonPropertyName("type")]` | `"identity"` | Тип объекта (переопределен) |
| `Name` | `string` | `[JsonPropertyName("name")]` | - | Название организации/субъекта |
| `IdentityClass` | `string` | `[JsonPropertyName("identity_class")]` | - | Класс идентификации |
| `Roles` | `List<string>` | `[JsonPropertyName("roles")]` | `new List<string>()` | Роли субъекта |
| `Sectors` | `List<string>` | `[JsonPropertyName("sectors")]` | `new List<string>()` | Отрасли деятельности |

---

## Класс `StixMalware` <a name="stixmalware"></a>

**Описание:**  
Представляет вредоносное ПО (malware) в модели MITRE ATT&CK. Описывает различные типы зловредных программ, используемых злоумышленниками.

### Общая информация
| Характеристика | Значение |
|---------------|----------|
| Пространство имен | `MitreAttackParser.Entities` |
| Наследование | `MitreStixObject` |
| Тип STIX | `malware` |
| Назначение | Описание вредоносных программ и их характеристик |

### Свойства класса
| Свойство | Тип | JSON-атрибут | По умолчанию | Описание |
|----------|-----|--------------|--------------|-----------|
| `Type` | `string` | `[JsonPropertyName("type")]` | `"malware"` | Тип объекта (переопределен) |
| `Name` | `string` | `[JsonPropertyName("name")]` | - | Название вредоносного ПО |
| `Description` | `string` | `[JsonPropertyName("description")]` | - | Подробное описание функционала |
| `MalwareTypes` | `List<string>` | `[JsonPropertyName("malware_types")]` | `new List<string>()` | Категории вредоносного ПО |
| `IsFamily` | `bool` | `[JsonPropertyName("is_family")]` | `false` | Флаг семейства malware |
| `MitreAliases` | `List<string>` | `[JsonPropertyName("x_mitre_aliases")]` | `new List<string>()` | Альтернативные названия |

---

## Класс `StixRelationship` <a name="stixrelationship"></a>

**Описание:**  
Представляет отношения между объектами в модели MITRE ATT&CK. Используется для установления связей между различными сущностями (тактиками, техниками, инструментами и т.д.).

### Общая информация
| Характеристика | Значение |
|---------------|----------|
| Пространство имен | `MitreAttackParser.Entities` |
| Наследование | `MitreStixObject` |
| Тип STIX | `relationship` |
| Назначение | Описание связей между объектами ATT&CK |

### Свойства класса
| Свойство | Тип | JSON-атрибут | По умолчанию | Описание |
|----------|-----|--------------|--------------|-----------|
| `Type` | `string` | `[JsonPropertyName("type")]` | `"relationship"` | Тип объекта (переопределен) |
| `RelationshipType` | `string` | `[JsonPropertyName("relationship_type")]` | - | Тип отношения |
| `SourceRef` | `string` | `[JsonPropertyName("source_ref")]` | - | Ссылка на исходный объект |
| `TargetRef` | `string` | `[JsonPropertyName("target_ref")]` | - | Ссылка на целевой объект |
| `Description` | `string` | `[JsonPropertyName("description")]` | - | Описание отношения |

---

## Класс `StixDataComponent` <a name="stixdatacomponent"></a>

**Описание:**  
Представляет компоненты данных (Data Components) в модели MITRE ATT&CK. Описывает конкретные элементы данных в рамках источника данных, которые могут быть использованы для обнаружения атак.

### Общая информация
| Характеристика | Значение |
|---------------|----------|
| Пространство имен | `MitreAttackParser.Entities` |
| Наследование | `MitreStixObject` |
| Тип STIX | `x-mitre-data-component` |
| Назначение | Детализация источников данных для обнаружения атак |

### Свойства класса
| Свойство | Тип | JSON-атрибут | По умолчанию | Описание |
|----------|-----|--------------|--------------|-----------|
| `Type` | `string` | `[JsonPropertyName("type")]` | `"x-mitre-data-component"` | Тип объекта (переопределен) |
| `Name` | `string` | `[JsonPropertyName("name")]` | - | Название компонента данных |
| `Description` | `string` | `[JsonPropertyName("description")]` | - | Подробное описание компонента |
| `MitreDataSourceRef` | `string` | `[JsonPropertyName("x_mitre_data_source_ref")]` | - | Ссылка на родительский источник данных |

---

## Класс `StixDataSource` <a name="stixdatasource"></a>

**Описание:**  
Представляет источники данных (Data Sources) в модели MITRE ATT&CK. Описывает типы данных и системы, которые могут быть использованы для обнаружения атак.

### Общая информация
| Характеристика | Значение |
|---------------|----------|
| Пространство имен | `MitreAttackParser.Entities` |
| Наследование | `MitreStixObject` |
| Тип STIX | `x-mitre-data-source` |
| Назначение | Описание источников данных для обнаружения атак |

### Свойства класса
| Свойство | Тип | JSON-атрибут | По умолчанию | Описание |
|----------|-----|--------------|--------------|-----------|
| `Type` | `string` | `[JsonPropertyName("type")]` | `"x-mitre-data-source"` | Тип объекта (переопределен) |
| `Name` | `string` | `[JsonPropertyName("name")]` | - | Название источника данных |
| `Description` | `string` | `[JsonPropertyName("description")]` | - | Подробное описание источника |
| `MitrePlatforms` | `List<string>` | `[JsonPropertyName("x_mitre_platforms")]` | `new List<string>()` | Поддерживаемые платформы |
| `MitreCollectionLayers` | `List<string>` | `[JsonPropertyName("x_mitre_collection_layers")]` | `new List<string>()` | Уровни сбора данных |

---

## Класс `StixAsset` <a name="stixasset"></a>

**Описание:**  
Представляет актив (asset) в модели данных MITRE ATT&CK. Содержит информацию об активах, их платформах, секторах и связанных активах.

### Общая информация
| Характеристика | Значение |
|---------------|----------|
| Пространство имен | `MitreAttackParser.Entities` |
| Наследование | `MitreStixObject` |
| Тип STIX | `x-mitre-asset` |
| Назначение | Представление актива в модели MITRE ATT&CK |

### Свойства класса
| Свойство | Тип | JSON-атрибут | По умолчанию | Описание |
|----------|-----|--------------|--------------|-----------|
| `Type` | `string` | `[JsonPropertyName("type")]` | `"x-mitre-asset"` | Тип объекта STIX (переопределен) |
| `Name` | `string` | `[JsonPropertyName("name")]` | - | Наименование актива |
| `Description` | `string` | `[JsonPropertyName("description")]` | - | Детальное описание |
| `Sectors` | `List<string>` | `[JsonPropertyName("x_mitre_sectors")]` | `new List<string>()` | Отрасли применения |
| `Platforms` | `List<string>` | `[JsonPropertyName("x_mitre_platforms")]` | `new List<string>()` | Поддерживаемые платформы |
| `RelatedAssets` | `List<RelatedAsset>` | `[JsonPropertyName("x_mitre_related_assets")]` | `new List<RelatedAsset>()` | Коллекция связанных активов |

### Вложенный класс `RelatedAsset`
**Описание:**  
Представляет связанный актив в модели MITRE ATT&CK. Используется для описания взаимосвязей между различными активами в системе.
| Свойство | Тип | JSON-атрибут | По умолчанию | Описание |
|----------|-----|--------------|--------------|-----------|
| `Name` | `string` | `[JsonPropertyName("name")]` | - | Наименование |
| `Description` | `string` | `[JsonPropertyName("description")]` | - | Описание |
| `Sectors` | `List<string>` | `[JsonPropertyName("related_asset_sectors")]` | `new List<string>()` | Отрасли применения |
