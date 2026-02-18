# AutomationExercise Playwright + NUnit Automation Framework

This repository contains a modern UI automation framework built with **Playwright**, **C#**, and **NUnit**.  
It is designed to be clean, scalable, and production-ready, following best practices for test structure, reporting, configuration, and maintainability.

---

## 🚀 Tech Stack

- **Playwright for .NET** – Browser automation
- **NUnit** – Test framework
- **ExtentReports** – HTML reporting with screenshots
- **.NET 8** – Runtime and project structure
- **C#** – Primary language

---


## 📁 Project Structure
```
AutomationExerciseDemo/
│
├── Config/
│   └── config.json
│
├── UI/
│   ├── Base/
│   │   └── BaseUiTest.cs
│   ├── Pages/
│   └── Tests/
│
├── Reports/
├── Screenshots/
└── AutomationExerciseDemo.csproj
```
---



## 🧪 Running Tests

From the project root, run:
dotnet test

Playwright will automatically:

- Launch the browser  
- Execute tests  
- Capture screenshots on failure  
- Generate an Extent HTML report  

---

## 📊 Reporting

After each test run, an HTML report is generated in:
/Reports/TestReport_<timestamp>.html


Screenshots on failure are saved in:
/Screenshots/

---

## 🔧 Configuration

The framework loads settings from:
Config/config.json


This includes:

- Base URL  
- Browser type  
- Headless mode  
- Timeout settings  

---

## 🛠 Future Enhancements (Roadmap)

- Add Page Object Models for all major flows  
- Add step‑logging helpers  
- Add retry logic for flaky tests  
- Add Playwright tracing/video recording  
- Add CI/CD pipeline (GitHub Actions)  
- Add parallel execution support  

---

## 📌 Purpose of This Project

This framework is part of my ongoing work to build clean, scalable, enterprise‑grade automation solutions.  
It demonstrates:

- Strong understanding of automation architecture  
- Modern tooling (Playwright + .NET)  
- Clean code practices  
- Reporting, configuration, and test lifecycle management  