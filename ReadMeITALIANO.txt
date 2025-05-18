==============================
 PirateOBDSimulator4KKL
==============================

📌 DESCRIZIONE
PirateOBDSimulator4KKL è un simulatore console per Windows scritto in C#.
Riproduce il comportamento di una centralina ECU Bosch EDC15C5 (Lancia Lybra 1.9 JTD)
per testare software diagnostici e di rimappatura che si interfacciano via KKL (protocolli K-Line / ISO 9141-2).

✅ Compatibile con software che utilizzano K-Line su porta COM.
✅ Ideale per test offline in ambiente sicuro.
✅ Nessuna ECU reale richiesta.

------------------------------
⚙️ DETTAGLI TECNICI
------------------------------
- Interfaccia: Porta seriale (COM) simulata
- Protocollo: ISO 9141-2 (K-Line)
- Baudrate: 10400 bps
- ECU simulata: Bosch EDC15C5 (256 KB memoria flash)

------------------------------
📜 COMANDI SUPPORTATI
------------------------------

1. 🆗 Inizializzazione ECU
   - Comando: 68 6A F1 01 00
   - Risposta: 48 6B 11 C1 00

2. ❗ Leggi Codici Errore (DTC)
   - Comando: 68 6A F1 03 00 00 00
   - Risposta: 48 6B 11 43 01 01 00 00
   - (Simula un DTC attivo)

3. 🧹 Cancella Codici Errore
   - Comando: 68 6A F1 04 00
   - Risposta: 48 6B 11 44 00

4. 🆔 Identificazione ECU
   - Comando: 68 6A F1 1A 80
   - Risposta: 48 6B 11 5A 80 45 44 43
     (Identificativo "EDC")

5. 💾 Lettura Memoria Flash
   - Comando: 68 6A F1 23 A0 00
   - Azione: Salva file binario chiamato "EDC15C5_FlashDump.bin"
   - Risposta: 48 6B 11 63 01

Nota: I comandi sono ricevuti in formato RAW binario via porta seriale.

------------------------------
📂 FILES GENERATI
------------------------------
- EDC15C5_FlashDump.bin → Dump Flash simulata (256 KB)

------------------------------
🛠️ NOTE DI COMPILAZIONE
------------------------------
- Linguaggio: C#
- Target: .NET Framework o .NET Core (console)
- SerialPort: Usa System.IO.Ports

------------------------------
🔐 FUTURE FEATURE IDEAS
------------------------------
- Seed & Key Security Access
- Lettura/Scrittura EEPROM
- Simulazione completa protocollo KW1281
- Checksums dinamici & mappe reali


