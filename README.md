# JsonEditor

Unity에서 JSON 파일을 더 쉽게 편집하고 관리할 수 있는 커스텀 인스펙터를 제공하는 라이브러리입니다.

*English version available in [README.en.md](README.en.md)*

## 개요

JsonEditor는 Unity의 자체 직렬화 시스템을 활용하여 JSON 파일 관리와 수정을 더 쉽게 만들어주는 라이브러리입니다. 직관적인 JSON 데이터 편집이 가능한 커스텀 인스펙터 인터페이스를 제공하며, 참조 관계를 유지할 수 있습니다.

## 특징

- Unity의 자체 직렬화 기능을 이용해서 JSON 파일을 보다 손쉽게 수정 및 관리 가능
- JSON에서 Addressable의 AssetReference의 참조 표현 가능
- JSON과 JSON 사이의 참조 관계를 표현할 수 있음, 이를 통해 높은 재사용성 확보
- 직관적인 편집을 위한 커스텀 인스펙터 인터페이스 제공

## 요구사항

### 의존성
- Newtonsoft.Json

### 제약사항
- 유니티 상에서 10depth가 넘는 JSON 파일은 표현할 수 없습니다.

## 지원하는 데이터 타입

### JsonObject

![JsonObject](https://github.com/user-attachments/assets/fdb2b991-02b8-46a1-8db9-a9de74ebf7d8)

JsonScriptableObject로 표현할 수 있는 형식은 다음과 같습니다:

| 타입 | 설명 |
|------|------|
| String | 문자열 데이터 표현이 가능합니다 |
| Int | 정수 데이터 표현이 가능합니다 |
| Float | 실수 데이터 표현이 가능합니다 |
| Bool | 논리 데이터 표현이 가능합니다 |
| Array | 값으로만 이루어진 배열 데이터 표현이 가능합니다. 인덱스로 접근합니다 |
| Object | 키와 값으로 이루어진 오브젝트 데이터 표현이 가능합니다. 키로 접근합니다 |
| Addressable | AddressableReference 데이터 표현이 가능합니다. 내부적으로 GUID를 저장합니다 |
| JsonObject | 또 다른 Json Object를 참조합니다. ReadOnlyJsonScriptableObject와 함께 사용할 때 유용합니다 |

### ReadOnlyJsonObject

![ReadOnlyJsonObject](https://github.com/user-attachments/assets/e87db9a7-aa49-42d3-8cbd-cd346a3fef06)

ReadOnlyJsonObject는 다음과 같은 추가 기능을 제공합니다:

- 다른 JsonScriptableObject를 Preset으로 사용하여 Key 값 수정을 방지하고 Value만 수정하도록 할 수 있습니다
- JSON File로 직렬화할 때, JsonObjectType에 대해 참조 경로가 아닌, 내부 JSON File을 그대로 노출합니다
- 데이터 구조의 일관성을 유지하면서 값만 수정할 수 있어 유용합니다

## 사용방법

1. Unity에서 새로운 JsonScriptableObject 또는 ReadOnlyJsonScriptableObject를 생성합니다
2. 커스텀 인스펙터를 사용하여 JSON 데이터를 수정합니다
3. ReadOnlyJsonScriptableObject의 경우, 프리셋을 설정하여 구조는 잠그고 값만 수정할 수 있도록 합니다
4. Unity 인스펙터 인터페이스를 통해 값을 접근하고 수정합니다
