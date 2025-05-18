
==============================
 PirateOBDSimulator4KKL
==============================

📌 DESCRIPTION
PirateOBDSimulator4KKL is a Windows console simulator written in C#.
It mimics the behavior of a Bosch EDC15C5 ECU (Lancia Lybra 1.9 JTD)
to test diagnostic and remapping software that interfaces via KKL (K-Line / ISO 9141-2 protocols).

✅ Compatible with software using K-Line on a COM port.
✅ Ideal for offline testing in a safe environment.
✅ No real ECU required.

------------------------------
⚙️ TECHNICAL DETAILS
------------------------------
- Interface: Simulated Serial COM port
- Protocol: ISO 9141-2 (K-Line)
- Baudrate: 10400 bps
- Simulated ECU: Bosch EDC15C5 (256 KB flash memory)

------------------------------
📜 SUPPORTED COMMANDS
------------------------------

1. 🆗 ECU Initialization
   - Command: 68 6A F1 01 00
   - Response: 48 6B 11 C1 00

2. ❗ Read Trouble Codes (DTC)
   - Command: 68 6A F1 03 00 00 00
   - Response: 48 6B 11 43 01 01 00 00
   - (Simulates one active DTC)

3. 🧹 Clear Trouble Codes
   - Command: 68 6A F1 04 00
   - Response: 48 6B 11 44 00

4. 🆔 ECU Identification
   - Command: 68 6A F1 1A 80
   - Response: 48 6B 11 5A 80 45 44 43
     (Identifier "EDC")

5. 💾 Flash Memory Read
   - Command: 68 6A F1 23 A0 00
   - Action: Saves binary file "EDC15C5_FlashDump.bin"
   - Response: 48 6B 11 63 01

Note: Commands are received as RAW binary over the serial port.

------------------------------
📂 GENERATED FILES
------------------------------
- EDC15C5_FlashDump.bin → Simulated Flash Dump (256 KB)

------------------------------
🛠️ BUILD NOTES
------------------------------
- Language: C#
- Target: .NET Framework or .NET Core (console)
- SerialPort: Uses System.IO.Ports

------------------------------
🔐 FUTURE FEATURE IDEAS
------------------------------
- Seed & Key Security Access
- EEPROM Read/Write
- Full KW1281 protocol simulation
- Dynamic checksums & real maps


