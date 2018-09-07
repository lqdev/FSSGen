# FSSGen - F# Static Site Generator

This project converts Makdown to HTML Files.

## Dependencies

- [Mono](https://www.mono-project.com/)
- [Fue](https://github.com/Dzoukr/Fue)
- [Fsharp.Formatting](https://github.com/fsprojects/FSharp.Formatting)
- [Paket](https://fsprojects.github.io/Paket/)

## Install

```bash
mono .paket/paket.exe install
```

## Getting Started

1. Create `Markdown` files inside of the `src` directory
2. In the command line type

```bash
fsharpi app.fsx build
```

3. The output from these files should be rendered as HTML inside the `public` directory

Features

- Convert Markdown to HTML
- Code Syntax Highlighting

