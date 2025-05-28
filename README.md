# dotnet-mitre-attack-parser

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/download/dotnet/8.0)

Библиотека для парсинга данных MITRE ATT&CK в объекты C#, включая поддержку всех основных типов объектов и опциональных компонентов.

## 📋 Содержание

- [Возможности](#features)
- [Установка](#installation)
- [Примеры использования](#usage-examples)
- [Документация](#documentation)
- [Лицензии](#licenses)
- [Правовая информация](#legal-information)
- [Вклад в проект](#contributing)

## ⚡ Возможности <a name="features"></a>

- **Полноценная десериализация** STIX 2.1 данных MITRE ATT&CK  
- **Автоматическое определение типов** объектов через поле `type`  
- **Поддержка всех MITRE-расширений** (`x_mitre_*` поля)  
- **Обработка вложенных структур**:
  - Ссылки на источники (`external_references`)  
  - Фазы Kill Chain (`kill_chain_phases`)  
  - Метки объектов (`object_marking_refs`)  

- **Поддержка всех типов объектов MITRE**:

  - **Матрицы** (`x-mitre-matrix`) 
  - **Коллекции** (`x-mitre-collection`) 
  - **Тактики** (`x-mitre-tactic`) 
  - **Техники** (`attack-pattern`) 
  - **Группы** (`intrusion-set`) 
  - **Инструменты** (`tool`)   
  - **Кампании** (`campaign`) 
  - **Меры защиты** (`course-of-action`) 
  - **Идентификаторы** (`identity`) 
  - **Вредоносное ПО** (`malware`) 
  - **Связи** (`relationship`) 
  - **Компоненты данных** (`x-mitre-data-component`) 
  - **Источники данных** (`x-mitre-data-source`) 
  - **Ассеты** (`x-mitre-asset`) 

> **Примечание**  
> Для опциональных типов (например, `x-mitre-asset`) перед использованием рекомендуется проверять `null`.  

## 📦 Установка <a name="installation"></a>

### NuGet Package

```bash
dotnet add package MitreAttackParser
```

### Из исходного кода

```bash
git clone https://github.com/Security-Experts-Community/dotnet-mitre-attack-parser.git
cd dotnet-mitre-attack-parser
dotnet build
```

## 🚀 Примеры использования <a name="usage-examples"></a>

### 1. Инициализация и загрузка данных
```csharp
using MitreAttackParser;

// Создание репозитория с настройками по умолчанию
var repository = new AttackRepository();

// Загрузка всех коллекций (Enterprise, ICS, Mobile)
if (await repository.CreateAsync())
{
    Console.WriteLine("Данные успешно загружены");
}
```

> **Примечание**  
> Перед обращениям к объектам `AttackRepository` рекомендуется дождаться окончания создания репозитория при помощи команды `Wait()`, или убедившись, что команда `CreateAsync()` вернула значение `true`.

### 2. Доступ к конкретным коллекциям
```csharp
// Получение всех объектов коллекции Enterprise
var enterpriseData = repository.EnterpriseCollection;
```

### 3. Поиск техники по MITRE ID
```csharp
// Получение информации о техники по её ID
var technique = repository.EnterpriseCollection.Techniques.FirstOrDefault(t => t.Id == "attack-pattern--00d0b012-8a03-410e-95de-5826bf542de6");
if (technique != null)
{
    Console.WriteLine($"Техника: {technique.Name}");
    Console.WriteLine($"Описание: {technique.Description}");
}
```

## 📚 Документация <a name="documentation"></a>

Подробная информация о классах и API доступна в [CLASSINFO.md](CLASSINFO.md).

## 📄 Лицензии <a name="licenses"></a>

### Основная лицензия
Этот проект распространяется под лицензией MIT. Подробности в файле [LICENSE.txt](LICENSE.txt).

### Дополнительные лицензии
- **MITRE ATT&CK®** - [MIT License](LICENSE-ATTACK.txt)
- **TAXII Server API** - [Apache 2.0 License](LICENSE-TAXII.txt)

## ⚖️ Правовая информация <a name="legal-information"></a>

Этот проект является независимой реализацией для работы с данными:
- [MITRE ATT&CK®](https://attack.mitre.org/) (MIT)
- [ATT&CK Workbench TAXII Server](https://github.com/mitre-attack/attack-workbench-taxii-server) (Apache 2.0)

⚠ **Отказ от ответственности**:  
Этот проект использует данные из MITRE ATT&CK®, но не является официальным продуктом MITRE.  
ATT&CK® является зарегистрированной торговой маркой The MITRE Corporation.  
Официальные данные ATT&CK доступны на [MITRE ATT&CK®](https://attack.mitre.org/).

MITRE® и ATT&CK® являются зарегистрированными товарными знаками The MITRE Corporation.

## 🤝 Вклад в проект <a name="contributing"></a>

Мы приветствуем вклад в развитие проекта! Пожалуйста, ознакомьтесь с [CONTRIBUTING.md](CONTRIBUTING.md) для получения информации о том, как внести свой вклад.
