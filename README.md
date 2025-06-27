# 🖼️ Sistema de Processamento de Imagens - Realce e Segmentação

[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![.NET Framework](https://img.shields.io/badge/.NET_Framework-4.8-512BD4?style=for-the-badge&logo=.net&logoColor=white)](https://dotnet.microsoft.com/)
[![Windows Forms](https://img.shields.io/badge/Windows_Forms-0078D4?style=for-the-badge&logo=windows&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/desktop/winforms/)

Um sistema completo de processamento de imagens desenvolvido em C# com Windows Forms, implementando algoritmos avançados de realce e segmentação de imagens através de técnicas de filtragem digital.

## 📋 Índice

- [Visão Geral](#-visão-geral)
- [Características](#-características)
- [Tecnologias Utilizadas](#-tecnologias-utilizadas)
- [Funcionalidades](#-funcionalidades)
- [Pré-requisitos](#-pré-requisitos)
- [Instalação](#-instalação)
- [Como Usar](#-como-usar)
- [Estrutura do Projeto](#-estrutura-do-projeto)
- [Algoritmos Implementados](#-algoritmos-implementados)
- [Exemplos Visuais](#-exemplos-visuais)

## 🎯 Visão Geral

Este projeto é uma aplicação desktop robusta para processamento digital de imagens, desenvolvida como parte de atividades acadêmicas (TTC - Listas 3 e 4). O sistema oferece uma interface intuitiva para aplicação de diversos filtros e algoritmos de processamento de imagens, incluindo técnicas de realce e segmentação.

### ✨ Destaques

- 🚀 **Performance Otimizada**: Implementação com acesso direto à memória (DMA) para processamento eficiente
- 🎨 **Interface Intuitiva**: GUI moderna com visualização lado-a-lado das imagens original e processada
- 🔧 **Algoritmos Avançados**: Implementação de técnicas clássicas e modernas de processamento de imagens
- 📊 **Análise Histográfica**: Geração e equalização de histogramas com visualização gráfica
- 🖼️ **Múltiplos Formatos**: Suporte para JPG, PNG, BMP e GIF

## 🛠️ Características

### Processamento Básico
- **Conversão para Escala de Cinza**: Algoritmo otimizado com pesos perceptuais
- **Negativo**: Inversão de intensidades com preservação de qualidade
- **Espelhamento Vertical**: Reflexão de imagens com precisão pixel-perfect

### Análise Histográfica
- **Geração de Histogramas**: Análise estatística da distribuição de intensidades
- **Equalização de Histogramas**: Melhoria automática de contraste
- **Visualização Gráfica**: Gráficos intuitivos salvos automaticamente

### Filtragem Espacial
- **Filtros de Suavização**: Média 5x5, Mediana 5x5, K-vizinhos mais próximos
- **Detecção de Bordas**: Operadores Roberts, Prewitt e Sobel
- **Filtros Passa-Alta**: Realce de detalhes com máscaras 4 e 8-conectadas
- **Operações Aritméticas**: Subtração de imagens para análise comparativa

### Análise Bit-Level
- **Fatiamento por Planos de Bits**: Decomposição em 8 camadas individuais
- **Análise de Significância**: Visualização da contribuição de cada bit

## 🔧 Tecnologias Utilizadas

- **Linguagem**: C# (.NET Framework 4.8)
- **Interface**: Windows Forms
- **Processamento de Imagem**: System.Drawing, System.Drawing.Imaging
- **Gerenciamento de Memória**: Ponteiros unsafe para performance otimizada
- **Formatos Suportados**: JPEG, PNG, BMP, GIF
- **IDE Recomendada**: Visual Studio 2019/2022

## 🎮 Funcionalidades

### 🔄 Processamento Dual
Cada algoritmo implementado oferece duas versões:
- **Versão Standard**: Usando métodos seguros do .NET
- **Versão DMA**: Acesso direto à memória para máxima performance

### 📊 Análise Quantitativa
- Cálculo automático de métricas de qualidade
- Geração de histogramas comparativos
- Estatísticas detalhadas de processamento

### 🎛️ Interface Avançada
- Visualização simultânea: imagem original vs processada
- Controles intuitivos para cada algoritmo
- Feedback visual em tempo real
- Sistema de logs para debugging

## 📋 Pré-requisitos

- **Sistema Operacional**: Windows 10/11 (x64)
- **.NET Framework**: 4.8 ou superior
- **Memória RAM**: Mínimo 4GB (recomendado 8GB)
- **Espaço em Disco**: 100MB livres
- **Resolução**: 1280x720 ou superior

## ⚡ Instalação

### Método 1: Executável (Recomendado)
```bash
# 1. Faça o download do projeto
git clone https://github.com/[seu-usuario]/processamento-imagens-realce-e-segmentacao.git

# 2. Navegue até a pasta de binários
cd processamento-imagens-realce-e-segmentacao/ProcessamentoImagens/bin/Debug/

# 3. Execute o aplicativo
ProcessamentoImagens.exe
```

### Método 2: Compilação Manual
```bash
# 1. Clone o repositório
git clone https://github.com/[seu-usuario]/processamento-imagens-realce-e-segmentacao.git

# 2. Abra o projeto no Visual Studio
# Arquivo: ProcessamentoImagens.sln

# 3. Configure para Debug/Release
# Build → Build Solution (Ctrl+Shift+B)

# 4. Execute o projeto
# Debug → Start Debugging (F5)
```

## 🎯 Como Usar

### 1️⃣ Carregamento de Imagem
1. Clique em **"Abrir Imagem"**
2. Selecione um arquivo de imagem (JPG, PNG, BMP, GIF)
3. A imagem aparecerá no painel esquerdo

### 2️⃣ Aplicação de Filtros
1. Escolha o algoritmo desejado nos botões disponíveis
2. A imagem processada aparecerá no painel direito
3. Compare visualmente os resultados

### 3️⃣ Análise de Histogramas
1. Use os botões de **"Histograma"** ou **"Histograma DMA"**
2. Os gráficos serão salvos automaticamente na pasta `bin/Debug/`
3. Visualize as melhorias de contraste aplicadas

### 4️⃣ Operações Avançadas
- **Fatiamento de Bits**: Gera 8 imagens individuais (uma para cada bit)
- **Detecção de Bordas**: Escolha entre Roberts, Prewitt ou Sobel
- **Filtros de Suavização**: Teste diferentes abordagens de redução de ruído

## 📁 Estrutura do Projeto

```
processamento-imagens-realce-e-segmentacao/
├── 📄 ProcessamentoImagens.sln          # Solução principal
├── 📁 ProcessamentoImagens/             # Código-fonte principal
│   ├── 🎨 frmPrincipal.cs              # Interface principal
│   ├── 🎨 frmPrincipal.Designer.cs     # Designer da interface
│   ├── 🔧 Filtros.cs                   # Algoritmos de processamento
│   ├── 🚀 Program.cs                   # Ponto de entrada da aplicação
│   ├── ⚙️ ProcessamentoImagens.csproj   # Configurações do projeto
│   ├── 📁 Properties/                   # Recursos e configurações
│   ├── 📁 bin/Debug/                    # Executáveis e resultados
│   └── 📁 obj/                          # Arquivos temporários de build
├── 📁 Imagens/                          # Imagens de teste incluídas
│   ├── 🖼️ fotoescura.jpg               # Imagem para teste de realce
│   ├── 🖼️ mathwork.png                 # Imagem técnica para segmentação
│   ├── 🖼️ wifi.jpg                     # Imagem para testes diversos
│   └── 🖼️ ...                          # Outras imagens de exemplo
└── 📖 README.md                         # Este arquivo
```

## 🧮 Algoritmos Implementados

### 🔄 Conversões Básicas
- **RGB → Escala de Cinza**: `I = 0.299R + 0.587G + 0.114B`
- **Negativo**: `I' = 255 - I`
- **Binarização**: Threshold adaptativo

### 📊 Equalização de Histograma
```csharp
// Função de transformação
T(r) = (L-1) * CDF(r)
// Onde CDF é a função de distribuição cumulativa
```

### 🎭 Filtros de Convolução

#### Detecção de Bordas - Roberts
```
Gx = [ 1  0]    Gy = [ 0  1]
     [ 0 -1]         [-1  0]
```

#### Detecção de Bordas - Prewitt
```
Gx = [-1  0  1]    Gy = [-1 -1 -1]
     [-1  0  1]         [ 0  0  0]
     [-1  0  1]         [ 1  1  1]
```

#### Detecção de Bordas - Sobel
```
Gx = [-1  0  1]    Gy = [-1 -2 -1]
     [-2  0  2]         [ 0  0  0]
     [-1  0  1]         [ 1  2  1]
```

### 🔧 Filtros de Suavização
- **Média 5x5**: Redução de ruído gaussiano
- **Mediana 5x5**: Preservação de bordas
- **K-Vizinhos**: Filtragem adaptativa

### ⚡ Filtros Passa-Alta
- **Centro 4**: Realce com 4-conectividade
- **Centro 8**: Realce com 8-conectividade

## 🖼️ Exemplos Visuais

![image](https://github.com/user-attachments/assets/99ab6a1c-f07d-42c2-bb80-16c7d7599a1d)


### Fatiamento de Bits
```
Bit 0 (LSB): Ruído e texturas finas
Bit 1-2:     Detalhes secundários
Bit 3-5:     Estruturas principais
Bit 6-7:     Formas dominantes (MSB)
```
