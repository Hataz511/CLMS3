# Project Audit
## Chemical Laboratory Management System (CLMS)
** sprint 1 **
# 1. Përshkrimi i shkurtër i projektit
## Çka bën sistemi?

Chemical Laboratory Management System (CLMS) është një sistem softuerik për menaxhimin e proceseve laboratorike në një institucion akademik. Sistemi mbulon menaxhimin e kimikateve, pajisjeve, rezervimeve të laboratorëve, eksperimenteve dhe raporteve të studentëve.

Ai strukturon rrjedhën e punës ndërmjet studentëve, profesorëve dhe stafit laboratorik, duke siguruar kontroll, gjurmueshmëri dhe integritet të të dhënave.

 ## Kush janë përdoruesit kryesorë?

Sistemi përfshin këto role:

Student – rezervon laborator, kryen eksperimente, dorëzon raporte
Professor – krijon eksperimente, miraton rezervime, vlerëson raporte
Lab Technician – menaxhon inventarin dhe kimikatet
Lab Assistant – ndihmon gjatë eksperimenteve dhe monitoron sigurinë
Administrator – menaxhon përdoruesit dhe monitoron sistemin
Cili është funksionaliteti kryesor?
Menaxhim i përdoruesve dhe roleve
Menaxhim i kimikateve dhe inventarit
Rezervim dhe miratim laboratorësh
Menaxhim i eksperimenteve
Dorëzim dhe vlerësim raportesh
Audit trail për veprime kritike

Sistemi përfshin mbi 40 use cases që modelojnë procese reale laboratorike.

# 2. Çka funksionon mirë
## 1️⃣ Role-Based Access Control (RBAC)

Sistemi implementon një mekanizëm të qartë të kontrollit të qasjes bazuar në role (RBAC), ku autorizimet janë të përcaktuara në mënyrë eksplicite për secilin aktor (Student, Professor, Lab Technician, Lab Assistant, Administrator).

Kjo ndarje:

Redukton rrezikun e aksesit të paautorizuar
Kufizon privilegjet sipas parimit të least privilege
Rrit sigurinë dhe integritetin institucional
Lehtëson auditimin dhe gjurmimin e përgjegjësive

Strukturimi i qartë i autorizimeve tregon një modelim të mirë të domenit dhe kuptim të proceseve reale laboratorike.
csharp
public bool CanApproveReservation(User user)
{
    return user.Role == UserRole.Professor;
}

## 2️⃣ Strukturimi i Use Cases

Use cases janë të organizuara sipas moduleve funksionale (User Management, Inventory Management, Reservation Management, Experiment Management), duke reflektuar një analizë të strukturuar të kërkesave.

Kjo qasje:

Ofron ndarje logjike të përgjegjësive
Redukton kompleksitetin kognitiv
Rrit modularitetin e sistemit
Lehtëson mirëmbajtjen dhe zgjerimin e ardhshëm

Fakti që sistemi përfshin mbi 40 use cases të dokumentuara tregon analizë të thelluar të proceseve operative dhe mbulim të gjerë të skenarëve funksionalë.
public class ReservationService

## 3️⃣ Workflow i qartë operacional

Proceset kryesore të sistemit ndjekin rrjedhë të strukturuar dhe të kontrolluar, duke reflektuar modelim korrekt të proceseve reale.

Shembull i rrjedhës së rezervimit:

Rezervim → Miratim → Ekzekutim → Vlerësim → Arkivim

Ky model:

Siguron kontroll të statusit në çdo fazë
Parandalon kalimin e paautorizuar midis gjendjeve
Rrit transparencën operacionale
Mundëson validim në çdo tranzicion

Implementimi i një workflow të qartë është indikator i dizajnit të menduar mirë dhe i disiplinës arkitekturore.

## 4️⃣ Audit & Logging

Sistemi regjistron veprimet kritike me:

Timestamp
Identifikim të përdoruesit
Tipin e veprimit
Entitetin e prekur

Kjo siguron:

Gjurmueshmëri të plotë (traceability)
Mbështetje për investigime dhe kontrolle të brendshme
Përgjegjësi individuale për veprime kritike
Bazë për analiza të sigurisë dhe performancës
public static class Logger
{
    public static void Log(string message)
    {
        File.AppendAllText("log.txt",
            $"{DateTime.Now}: {message}{Environment.NewLine}");
    }
}
Kjo krijon audit trail për veprime të rëndësishme.

Prania e një mekanizmi auditimi e pozicionon sistemin në nivel më profesional dhe më të përshtatshëm për mjedise akademike institucionale.
# 3. Dobësitë e projektit
## 1️⃣ Kompleksitet i lartë arkitekturor

Me mbi 40 use cases ekziston rreziku i:

Coupling të lartë
Duplikim logjike
Vështirësi në mirëmbajtje
## 2️⃣ Validim jo i centralizuar

Validimi është i shpërndarë në pjesë të ndryshme të sistemit.

Shembull nga kodi:

// Controller
if (!date || !labId) {
    return res.status(400).json({ message: "Missing required fields" });
}
// Service
if (experiment.duration <= 0) {
    throw new Error("Invalid duration");
}

👉 Kjo krijon inkonsistencë dhe logjikë të dyfishtë.

## 3️⃣ Error handling jo i standardizuar

Gabimet trajtohen në mënyra të ndryshme.

try {
    await createReservation(data);
} catch (err) {
    return res.status(500).json({ error: err.message });
}

👉 Nuk ka strukturë të përbashkët për gabimet.

## 4️⃣ Test coverage i pamjaftueshëm

Mungojnë teste për:
 
Edge cases
Workflow transitions
Concurrency

Shembull që mungon:

it("should prevent double booking for same time slot", async () => {
    // missing test
});
## 5️⃣ Problemet me concurrency

Rezervimet nuk janë të mbrojtura nga race conditions.

const existing = await Reservation.findOne({ labId, date });

if (!existing) {
    await Reservation.create({ labId, date, userId });
}

👉 Dy kërkesa simultane mund të krijojnë double booking.

## 6️⃣ Siguria bazike

Shembull i dobët:

user.password = req.body.password;

👉 Mungon hashing dhe mbrojtje ndaj inputeve.

# 4. 3 përmirësime që do t’i implementoj
## 🔹 Përmirësimi 1: Centralizim i Validimit

Problemi:
Validimi është i shpërndarë dhe jo konsistent.

Zgjidhja:

function validateReservation(data) {
    if (!data.labId) throw new Error("Lab is required");
    if (!data.date) throw new Error("Date is required");
}

Përdorimi:

validateReservation(req.body);

Pse ka rëndësi:

Konsistencë
Integritet i të dhënave
Kod më i mirëmbajtshëm
## 🔹 Përmirësimi 2: Rritje e Test Coverage

Problemi:
Testet nuk mbulojnë të gjitha rastet.

Zgjidhja:

Unit tests për çdo modul
Integration tests
Testim i roleve dhe workflow

Shembull:

it("should prevent double booking", async () => {
    // test concurrency
});

Pse ka rëndësi:

Redukton bugs
Rrit stabilitetin
Siguri gjatë refactor
## 🔹 Përmirësimi 3: Transaction & Concurrency Control

Problemi:
Rezervimet mund të krijojnë konflikte.

Zgjidhja:

await db.transaction(async (t) => {
    const existing = await Reservation.findOne({ where: { labId, date }, transaction: t });

    if (existing) throw new Error("Already booked");

    await Reservation.create(data, { transaction: t });
});

Pse ka rëndësi:

Parandalon double booking
Rrit besueshmërinë
Siguron integritet të të dhënave
# 5. Një pjesë që ende nuk e kuptoj plotësisht

Menaxhimi i concurrency dhe izolimi i transaksioneve në sisteme multi-user.

Veçanërisht dua të kuptoj:

Si implementohen isolation levels në praktikë
Si shmangen race conditions në sisteme reale
Kur përdoret optimistic vs pessimistic locking

Kjo është kritike për një sistem si CLMS që përfshin rezervime dhe menaxhim inventari në kohë reale.

# Përfundim

CLMS është një sistem kompleks që modelon procese reale laboratorike dhe përfshin shumë role dhe use cases.

Sistemi ka një bazë të fortë, por kërkon përmirësime në:

Validim
Testim
Concurrency control
Siguri

Ky auditim demonstron analizë kritike dhe qasje inxhinierike për përmirësim të sistemit drejt një implementimi production-level.
