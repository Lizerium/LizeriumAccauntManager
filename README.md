<h1 align="center">🐇 Player Account Manager (DCAM)</h1>

<p align="center">
  <b>A modern reimagined player account manager for Freelancer Server, redesigned for large-scale game builds, Unicode support, asynchronous processing, and extensible architecture.</b>
</p>

<p align="center">
  <img src="https://shields.dvurechensky.pro/badge/Platform-Windows-0078D6?style=for-the-badge" />
  <img src="https://shields.dvurechensky.pro/badge/Framework-.NET-5C2D91?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://shields.dvurechensky.pro/badge/Architecture-MVP%20%2B%20Services-1565C0?style=for-the-badge" />
  <img src="https://shields.dvurechensky.pro/badge/Data-POCO%20%2B%20DTO-6A1B9A?style=for-the-badge" />
  <img src="https://shields.dvurechensky.pro/badge/Encoding-Unicode%20Ready-00C853?style=for-the-badge" />
  <img src="https://shields.dvurechensky.pro/badge/Status-Active%20Development-FF6D00?style=for-the-badge" />
</p>

<div align="center" style="margin: 20px 0; padding: 10px; background: #1c1917; border-radius: 10px;">
  <strong>🌐 Language: </strong>
  
  <a href="./README.ru.md" style="color: #F5F752; margin: 0 10px;">
    🇷🇺 Russian
  </a>
  | 
  <span style="color: #0891b2; margin: 0 10px;">
    ✅ 🇺🇸 English (current)
  </span>
</div>

---

> [!NOTE]
> This project is part of the **Lizerium** ecosystem and belongs to the following direction:
>
> - [`Lizerium.Software.Structs`](https://github.com/Lizerium/Lizerium.Software.Structs)
>
> If you are looking for related engineering and supporting tools, start there.

> [!NOTE]
> This project is a **reimagining and modernization** of an older player account management tool for **Freelancer (2003)** servers.
>
> It is being developed as a more **modern, scalable, and extensible version** of the classic **DS Account Manager / DCAM**.

## Credits

> [!NOTE]
> This project is based on work from the Freelancer community.
> Reworked and integrated into Lizerium ecosystem.
>
> Contributors: `Haenlomal`, `sherlog@t-online.de`

---

# 📖 About the Project

**DCAM (Dvurechensky Account Manager / DS Account Manager Rework)** is a redesigned tool for analyzing, visualizing, and managing player account data on a **Freelancer** server.

Legacy account managers were originally created in an era with very different constraints:

- smaller game builds
- simpler data structures
- limited encoding requirements
- minimal architectural expectations

Over time, it became clear that the old approach:

- **does not scale well**
- **breaks on large modifications**
- **fails with non-Latin encodings (e.g., Cyrillic)**
- **is difficult to extend**
- and **does not fit modern infrastructure needs**

As a result, the project was **rebuilt from scratch** with a focus on:

- clean and maintainable architecture
- support for large-scale game data
- modern .NET practices
- proper Unicode handling
- asynchronous processing
- extensibility for future services

---

# 🌖 Background

I, **[Dvurechensky](https://dvurechensky.pro)**, decided to **rebuild this tool almost from scratch**, because the original approach no longer matched the real-world requirements of my project.

Historically, the tool was known as:

- **DCAM**
- **DC Account Manager**
- **DS Account Manager**

However, it was designed for significantly smaller datasets and simpler game environments than those I work with today.

---

# ❗ Why the project had to be rebuilt

## 🌀 1. Extremely slow processing on large game builds

My game — and even the base **Freelancer** project — could take **10+ minutes** to process.

This is unacceptable for modern workflows, especially when the tool is used regularly as part of a larger pipeline.

---

## 🌀 2. Original tool was English-only

The original tool was designed for an English-only environment and lacked proper localization support.

I needed a tool that works naturally in a multilingual environment, including Russian.

---

## 🌀 3. Encoding and Cyrillic issues

When processing game data containing Cyrillic text, the old tool produced:

- `????????????`
- corrupted symbols
- broken text formatting

This made parts of the data unreadable or unusable.

---

## 🌀 4. Outdated XSD-based approach

The original tool relied heavily on **XSD-based structures**.

This approach:

- is outdated
- difficult to maintain
- does not scale well
- and **does not align with modern .NET development practices**

---

## 🌀 5. Mixed logic with no separation of concerns

The old project suffered from a classic issue:

- business logic
- data handling
- UI logic
- event handling

were often all placed in a single file.

For example, `MainWindow.cs` could easily exceed **2000+ lines** of mixed responsibilities.

This made:

- maintenance
- testing
- refactoring
- feature development

significantly harder.

---

## 🌀 6. Sequential algorithms slowed everything down

Most logic was executed **synchronously and sequentially**.

This resulted in:

- blocked UI
- poor performance on large datasets
- lack of scalability
- painful future development

---

## 🌀 7. No integration with modern ecosystem

The original tool was too isolated and difficult to integrate into a larger system.

I needed integration with:

- custom **anti-cheat services**
- internal tools
- synchronization with the **Lizerium portal**
- and a broader engineering ecosystem

---

# 🚀 What was changed

This is not a minor refactor — the project was **fundamentally redesigned**.

## 🐳 1. Move to asynchronous processing

The project was migrated from a **synchronous execution model** to an **asynchronous architecture**.

This enables:

- non-blocking UI
- faster processing
- better scalability
- readiness for heavy workloads

---

## 🐳 2. Full removal of `System.Data.DataSet`

The old stack:

- `DataSet`
- `DataTable`
- XSD designers

was completely removed.

Reason:

> XSD designers are not supported in modern .NET versions (.NET Core / 5 / 6 / 7 / 8)

---

## 🐳 3. Transition to `List<T>` and POCO models

The project now uses:

- `List<T>`
- **POCO classes**
- **DTO models**

This provides:

- clearer data structures
- better serialization/deserialization
- improved testability
- easier refactoring
- modern engineering practices

---

## 🐳 4. Architecture redesigned to `MVP + Services`

The project now follows:

- **MVP pattern**
- service layer separation
- clear responsibility boundaries
- extensible architecture

Principles applied:

- **SOLID**
- UI / logic separation
- testability
- component reuse

---

# 🧠 Architectural Concept

## `Presenter` controls the logic

Responsible for:

- workflows
- user actions
- service coordination
- data transformation

It must remain independent from specific UI implementations.

---

## `View` handles only UI

Responsible for:

- rendering
- UI events
- forwarding user actions

No business logic should live here.

---

## `IMainView` decouples Presenter from UI

This allows:

- testability
- reusability
- UI framework independence

---

# 🧱 Technical Improvements

The project is evolving beyond a simple account manager into a solid engineering foundation.

## Planned directions include:

- faster account processing
- full Unicode support
- deeper game entity analysis
- integration with internal services
- anti-cheat integration
- synchronization with **Lizerium infrastructure**
- modular rework of legacy components

---

# 📜 History & Origin

Based on the legacy tool:

## `DS Account Manager` — v1.3

Previously developed and maintained by:

- `cannon`
- `josh`
- `ktstgt`

This project aims not to replace its legacy, but to **preserve its core ideas and evolve them into a modern system**.

---

# ⚠️ Current State

> [!WARNING]
> The project is likely in **active development**.

This means:

- some features may be missing
- some functionality may still be in migration
- internal components may be under redesign

---

## 🐌 P.S.

If you want to contribute, you are welcome to:

- explore the architecture
- suggest improvements
- help restore missing features

---

# 🔗 Legacy Algorithms

## 🌲 Item hashing

Likely based on:

- `flhash.exe`
- author: `sherlog@t-online.de`
- date: `2003-06-11`

---

## 🌲 Faction hashing

Likely based on:

- `flfachash.exe`
- author: `Haenlomal`
- date: `October 2006`

---

# 📜 Changelog

See: [`CHANGELOG.md`](CHANGELOG.md)

---

# ⚖️ License

See: [`LICENSE`](LICENSE)

---

# 💬 Note

This is not just a rewritten legacy tool.

It is an attempt to:

- preserve valuable engineering ideas
- eliminate technical debt
- modernize architecture
- and prepare the tool for real-world usage in large-scale game ecosystems

What used to be a simple utility is now evolving into a **fully-fledged, extensible, and engineering-grade application**.
