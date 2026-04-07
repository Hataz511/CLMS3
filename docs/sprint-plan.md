# Sprint 2 Plan — Hata
Data: 1 Prill 2026

## Gjendja Aktuale

- Programi ka strukturë të organizuar me Models, Services dhe Repository
- Ekzistojnë disa funksione bazike si shtimi dhe menaxhimi i të dhënave
- Repository layer është implementuar për lexim/shkrim nga file

Çka nuk funksionon:
- Nuk ekziston funksionalitet kërkimi (search)
- Error handling është i kufizuar dhe mund të ndodhin crash
- Nuk ka unit tests

A kompajlohet dhe ekzekutohet programi?
- Po

## Plani i Sprintit

### Feature e Re (çka do të ndërtosh)

Do të implementoj funksionalitetin e kërkimit (Search) për Chemical.

Useri do të shkruajë emrin e chemical-it në UI dhe programi do të kërkojë në listë dhe do të shfaqë rezultatin nëse ekziston.

Flow:
UI → Service → Repository

### Error Handling (çka do të shtosh)

Do të shtoj error handling në këto raste:

1. File mungon:
- Në Repository do përdor try-catch për File.ReadAllText
- Nëse file nuk ekziston, do krijohet automatikisht

2. Input i gabuar:
- Në UI do kontrolloj input (null ose bosh)
- Do shfaq mesazh: "Ju lutem shkruani input valid"

3. Item nuk ekziston:
- Në Service do kontrolloj nëse rezultati është null
- Do shfaq mesazh: "Chemical nuk u gjet"

### Teste (çka do të testosh)

Do të testoj:

- Metodën Search (kur ekziston dhe kur nuk ekziston)
- Metodën Add (input valid dhe input bosh)

Raste kufitare:
- Emër bosh
- Emër që nuk ekziston
- Lista bosh

## Afati

- Deadline: Martë, 8 Prill 2026, ora 08:30