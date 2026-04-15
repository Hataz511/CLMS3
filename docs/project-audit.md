# Project Audit

**Chemical Laboratory Management System (CLMS)**
**Sprint 1**

---

## 1. Përshkrimi i shkurtër i projektit

### Çka bën sistemi?

Chemical Laboratory Management System (CLMS) është një sistem softuerik për menaxhimin e proceseve laboratorike në një institucion akademik. Sistemi mbulon menaxhimin e kimikateve, pajisjeve, rezervimeve të laboratorëve, eksperimenteve dhe raporteve të studentëve.

Ai strukturon rrjedhën e punës ndërmjet studentëve, profesorëve dhe stafit laboratorik, duke siguruar kontroll, gjurmueshmëri dhe integritet të të dhënave.

---

### Kush janë përdoruesit kryesorë?

* **Student** – rezervon laborator, kryen eksperimente, dorëzon raporte
* **Professor** – krijon eksperimente, miraton rezervime, vlerëson raporte
* **Lab Technician** – menaxhon inventarin dhe kimikatet
* **Lab Assistant** – ndihmon gjatë eksperimenteve dhe monitoron sigurinë
* **Administrator** – menaxhon përdoruesit dhe monitoron sistemin

---

### Cili është funksionaliteti kryesor?

* Menaxhim i përdoruesve dhe roleve (RBAC)
* Menaxhim i kimikateve dhe inventarit
* Rezervim dhe miratim laboratorësh
* Menaxhim i eksperimenteve
* Dorëzim dhe vlerësim raportesh
* Audit trail për veprime kritike

Sistemi përfshin mbi **40 use cases**, duke modeluar procese reale laboratorike.

---

## 2. Çka funksionon mirë

### 1️⃣ Role-Based Access Control (RBAC)

Sistemi implementon kontroll të qartë të qasjes bazuar në role.

```csharp
public bool CanApproveReservation(User user) {
    return user.Role == UserRole.Professor;
}
```

**Përfitimet:**

* Redukton aksesin e paautorizuar
* Zbaton parimin e *least privilege*
* Rrit sigurinë dhe integritetin
* Lehtëson auditimin

---

### 2️⃣ Strukturimi i Use Cases

Use cases janë të ndara në module:

* User Management
* Inventory Management
* Reservation Management
* Experiment Management

```csharp
public class ReservationService
```

**Përfitimet:**

* Modularitet i lartë
* Reduktim i kompleksitetit
* Mirëmbajtje më e lehtë
* Skalueshmëri në të ardhmen

---

### 3️⃣ Workflow i qartë operacional

Proceset ndjekin një rrjedhë të strukturuar:

**Rezervim → Miratim → Ekzekutim → Vlerësim → Arkivim**

**Përfitimet:**

* Kontroll i qartë i statuseve
* Parandalim i gabimeve logjike
* Transparencë në procese
* Validim në çdo fazë

---

### 4️⃣ Audit & Logging

```csharp
public static class Logger {
    public static void Log(string message) {
        File.AppendAllText("log.txt", $"{DateTime.Now}: {message}{Environment.NewLine}");
    }
}
```

**Përfitimet:**

* Gjurmueshmëri (traceability)
* Kontroll dhe investigim
* Përgjegjësi individuale
* Bazë për analiza të sigurisë

---

## 3. Dobësitë e projektit

### 1️⃣ Kompleksitet i lartë arkitekturor

* Rrezik për coupling të lartë
* Duplikim logjike
* Vështirësi në mirëmbajtje

---

### 2️⃣ Validim jo i centralizuar

```javascript
// Controller
if (!date || !labId) {
  return res.status(400).json({ message: "Missing required fields" });
}

// Service
if (experiment.duration <= 0) {
  throw new Error("Invalid duration");
}
```

👉 Logjikë e shpërndarë dhe jo konsistente

---

### 3️⃣ Error handling jo i standardizuar

```javascript
try {
  await createReservation(data);
} catch (err) {
  return res.status(500).json({ error: err.message });
}
```

👉 Nuk ka strukturë të përbashkët për gabimet

---

### 4️⃣ Test coverage i pamjaftueshëm

Mungojnë teste për:

* Edge cases
* Workflow transitions
* Concurrency

```javascript
it("should prevent double booking for same time slot", async () => {
  // missing test
});
```

---

### 5️⃣ Problemet me concurrency

```javascript
const existing = await Reservation.findOne({ labId, date });

if (!existing) {
  await Reservation.create({ labId, date, userId });
}
```

👉 Mund të ndodhë **double booking**

---

### 6️⃣ Siguria bazike

```javascript
user.password = req.body.password;
```

👉 Mungon:

* hashing i password-it
* validim inputi

---

### 7️⃣ UI Flow jo i optimizuar

* Navigimi nuk është intuitiv
* Kërkon shumë hapa për operacione të thjeshta

👉 Ndikon negativisht në eksperiencën e përdoruesit

---

## 4. 3 përmirësime që do t’i implementoj

### 🔹 Përmirësimi 1: Centralizim i Validimit

**Problemi:**
Validimi është i shpërndarë dhe jo konsistent

**Zgjidhja:**

```javascript
function validateReservation(data) {
  if (!data.labId) throw new Error("Lab is required");
  if (!data.date) throw new Error("Date is required");
}
```

**Pse ka rëndësi:**

* Konsistencë
* Integritet i të dhënave
* Kod më i mirëmbajtshëm

---

### 🔹 Përmirësimi 2: Rritje e Test Coverage

**Problemi:**
Testet nuk mbulojnë të gjitha rastet

**Zgjidhja:**

* Unit tests për çdo modul
* Integration tests
* Testim i roleve dhe workflow

```javascript
it("should prevent double booking", async () => {
  // concurrency test
});
```

**Pse ka rëndësi:**

* Redukton bugs
* Rrit stabilitetin
* Siguri gjatë refactor

---

### 🔹 Përmirësimi 3: Transaction & Concurrency Control

**Problemi:**
Rezervimet krijojnë konflikte

**Zgjidhja:**

```javascript
await db.transaction(async (t) => {
  const existing = await Reservation.findOne({
    where: { labId, date },
    transaction: t
  });

  if (existing) throw new Error("Already booked");

  await Reservation.create(data, { transaction: t });
});
```

**Pse ka rëndësi:**

* Parandalon double booking
* Rrit besueshmërinë
* Siguron integritet të të dhënave

---

## 5. Një pjesë që ende nuk e kuptoj plotësisht

Menaxhimi i concurrency dhe izolimi i transaksioneve në sisteme multi-user.

Veçanërisht:

* Si funksionojnë isolation levels në praktikë
* Si shmangen race conditions në sisteme reale
* Kur përdoret **optimistic vs pessimistic locking**

**Shembull konkret:**
Në rastin kur dy studentë bëjnë rezervim në të njëjtën kohë, nuk është plotësisht e qartë si databaza vendos cili transaksion ekzekutohet i pari në nivelin *READ COMMITTED*.

---

## Përfundim

CLMS është një sistem kompleks që modelon procese reale laboratorike dhe përfshin shumë role dhe use cases.

Sistemi ka një bazë të fortë, por kërkon përmirësime në:

* Validim
* Testim
* Concurrency control
* Siguri

Ky auditim demonstron analizë kritike dhe qasje inxhinierike për përmirësim të sistemit drejt një implementimi **production-level**.

