# JsonEditor

A Unity library that provides a custom inspector for easier JSON file editing and management.

*한국어 버전은 [README.md](README.md)에서 확인하실 수 있습니다*

## Overview

JsonEditor is a library that makes JSON file management and modification easier using Unity's built-in serialization system. It provides a custom inspector interface that allows for intuitive editing of JSON data while maintaining reference relationships.

## Features

- Easy modification and management of JSON files using Unity's native serialization system
- Support for Addressable's AssetReference representation in JSON
- Ability to express reference relationships between JSON files, ensuring high reusability
- Custom inspector interface for intuitive editing

## Requirements

### Dependencies
- Newtonsoft.Json

### Limitations
- Cannot represent JSON files with more than 10 depth levels in Unity

## Supported Data Types

### JsonObject

![JsonObject](https://github.com/user-attachments/assets/fdb2b991-02b8-46a1-8db9-a9de74ebf7d8)

JsonScriptableObject supports the following data types:

| Type | Description |
|------|-------------|
| String | Supports string data representation |
| Int | Supports integer data representation |
| Float | Supports floating-point data representation |
| Bool | Supports boolean data representation |
| Array | Supports array data representation with value-only arrays (accessed by index) |
| Object | Supports object data representation with key-value pairs (accessed by key) |
| Addressable | Supports AddressableReference data representation (stores GUID internally) |
| JsonObject | References another Json Object (useful when used with ReadOnlyJsonScriptableObject) |

### ReadOnlyJsonObject

![ReadOnlyJsonObject](https://github.com/user-attachments/assets/e87db9a7-aa49-42d3-8cbd-cd346a3fef06)

The ReadOnlyJsonObject provides additional functionality:

- Allows using other JsonScriptableObjects as presets
- Prevents key modifications while allowing value modifications
- When serializing to JSON File, exposes the internal JSON File directly for JsonObjectType rather than using reference paths
- Particularly useful for maintaining data structure consistency while allowing value modifications

## Usage

1. Create a new JsonScriptableObject or ReadOnlyJsonScriptableObject in Unity
2. Use the custom inspector to modify your JSON data
3. For ReadOnlyJsonScriptableObject, set a preset to lock the structure while allowing value modifications
4. Access and modify values through the Unity Inspector interface