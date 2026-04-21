
# Chemical Laboratory Management System (CLMS)

## Improvement Report

---

# 1️⃣ Executive Summary

Ky dokument paraqet një analizë kritike dhe përmirësim të thelluar të projektit Chemical Laboratory Management System (CLMS).

Qëllimi i këtij sprinti nuk ishte shtimi i funksionaliteteve të reja, por:

* Identifikimi i dobësive arkitekturore
* Përmirësimi i strukturës së kodit
* Rritja e reliability dhe robustness
* Përmirësimi i dokumentimit teknik
* Demonstrimi i mendimit kritik inxhinierik

Janë realizuar 3 përmirësime kryesore:

* Structural / Architectural Refactor
* Reliability & Validation Hardening
* Documentation & Engineering Transparency

👉 **Rezultati:** Sistemi është transformuar nga një implementim bazik në një arkitekturë modulare, të testueshme dhe të gatshme për zgjerim production-grade.

---

# 2️⃣ Improvement 1 – Structural Refactor & Architectural Hardening

## 📌 Problemi Fillestar

Në versionin fillestar:

* `FileRepository` përmbante:

  * File IO
  * Parsing CSV
  * Error handling
* Nuk kishte abstraction për file handling
* Tight coupling me CSV
* Validim i shpërndarë

👉 **Pasoja:**

* Shkelje e SRP (Single Responsibility Principle)
* Vështirësi në testim
* Vështirësi në migrim në database

---

## 🔧 Çfarë U Ndryshua

### ✔ Ndarja e përgjegjësive

* `IFileStorage` → File operations
* `Repository` → Data mapping + CRUD
* `Service` → Business logic + validation
* `UI` → Interaction me përdoruesin

---

## 🔄 Before vs After (Concrete Example)

**Before:**

* Repository menaxhonte gjithçka (file, parsing, validation)

**After:**

* FileStorage → vetëm file IO
* Repository → vetëm data mapping
* Service → validation dhe logjikë

👉 Eliminim i coupling dhe rritje modulariteti

---

## 💉 Dependency Injection

Sistemi tani përdor DI për loose coupling:

* Mundëson mocking
* Lehtëson testimin
* Lejon zëvendësim të implementimeve

---

## 🧠 Pse është më i mirë

* ✔ Respekton SRP dhe DIP
* ✔ Rrit testueshmërinë
* ✔ Lehtëson zgjerimin (p.sh. DB)

---

# 3️⃣ Improvement 2 – Reliability & Validation Hardening

## 📌 Problemi Fillestar

* File që mungon → crash
* ID jo-ekzistuese → exception i paqartë
* Pa validim inputesh
* Pa error handling

👉 Sistemi ishte i brishtë dhe jo user-friendly

---

## 🔧 Çfarë U Ndryshua

### ✔ File handling i sigurt

* Krijohet file automatikisht nëse mungon

### ✔ Validim në Service Layer

* Kontroll për:

  * emër bosh
  * çmim negativ
  * sasi negative

### ✔ Trajtim i ID jo-ekzistues

* Exception i qartë (`KeyNotFoundException`)

### ✔ Error handling në UI

* Try-catch për të shmangur crash

---

## 🧪 Unit Testing (NEW – Critical Improvement)

Për të garantuar correctness, u shtuan teste bazë:

```csharp
[Test]
public void AddChemical_ShouldThrow_WhenNameIsEmpty()
{
    var service = new ChemicalService(mockRepo);

    Assert.Throws<ArgumentException>(() =>
        service.AddChemical(new Chemical { Name = "" }));
}
```

```csharp
[Test]
public void GetById_ShouldThrow_WhenNotFound()
{
    Assert.Throws<KeyNotFoundException>(() =>
        service.GetById(999));
}
```

👉 **Impact:**

* Parandalon regresionet
* Validon business logic
* Rrit besueshmërinë e sistemit

---

## 🧠 Pse është më i mirë

* ✔ Nuk crash-on
* ✔ Jep feedback të qartë
* ✔ Është më i sigurt dhe stabil

---

# 4️⃣ Improvement 3 – Documentation & Engineering Transparency

## 📌 Problemi Fillestar

* Dokumentim sipërfaqësor
* Pa shpjegim të vendimeve

---

## 🔧 Çfarë U Shtua

### ✔ architecture.md

* Diagram i layer-ve
* Flow i dependencies

### ✔ Design Decisions

* CSV për thjeshtësi
* DB për production

### ✔ Known Limitations

* No concurrency
* No logging
* No transactions

### ✔ Setup Instructions (NEW)

* Si të ekzekutohet projekti
* Si të strukturohen files

---

## 🧠 Pse është më i mirë

* ✔ Lehtëson onboarding
* ✔ Rrit transparencën
* ✔ Reflekton mendim inxhinierik

---

# 5️⃣ Reflection – Critical Thinking

Ky sprint tregoi që:

* Kod që funksionon ≠ kod i mirë
* Arkitektura është po aq e rëndësishme sa implementimi

👉 **Gabim konkret:**
Validimi fillimisht ishte në UI → shpërndarje e logjikës
Zgjidhja → centralizim në Service layer

👉 **Çfarë mësova:**

* Abstraction është kritik për scalability
* Separation of concerns rrit maintainability
* Defensive programming parandalon bug-e
* Refactoring është proces i vazhdueshëm

---

# 6️⃣ Remaining Weaknesses

## High Priority

* Logging (Serilog / NLog)
* Test coverage më i gjerë

## Medium

* Concurrency control
* Data persistence me DB

## Low

* Authentication

---

# 7️⃣ Final Conclusion

Ky sprint nuk ishte për shtim features, por për:

* Ristrukturim arkitekturor
* Rritje të reliability
* Përmirësim të dokumentimit

👉 Sistemi tani është:

* Modular
* I testueshëm
* Robust
* I gatshëm për zgjerim

Ky refactor e transformon projektin nga një aplikacion akademik në një bazë solide për një sistem production-grade.

---
