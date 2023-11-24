# Language Course API
O que é: Uma API que guarda as informações de Aluno, Turma e Matrícula para um curso de idiomas genérico.

### Regras de Negócio:
- Aluno não pode ser cadastrado repetido (validação pelo CPF);
- No momento de cadastrar um aluno, deve-se informar pelo menos uma turma que ele irá cursar; 
- O mesmo aluno pode ser matriculado em várias turmas diferentes, porém a Matrícula não pode ser repetida na mesma turma; 
- Uma turma não pode ter mais de 5 alunos; 
- Turma não pode ser excluída se possuir alunos;

### Como rodar:
Usando o Visual Studio, basta abrir a LanguageCourse.sln e usar o ISS Express. A API irá abrir com o Swagger para que os endpoints sejam testados.
A API foi feita usando a versão 6.0 do .NET.
OBS: O sqlite está configurado para testar a API em modo de desenvolvimento.

Para um melhor teste das ferramentas, o ideal é criar uma Turma primeiro (Academic Class) para em seguida criar um Aluno (Student). A tabela Matrícula (Enrollment) será preenchida automaticamente, mas é possível adicionar dados à ela manualmente também.
Caso queira visualizar melhor os dados dentro da tabela, é possível usar aplicativos como o DB Browser. 

### Estrutura:
A API foi dividida em 4 camadas:
- API: Camada onde o usuário se comunica com a API através dos Controllers;
- Application: Camada onde estão localizadas as regras de negócio;
- Infrastructure: Camada responsável por se comunicar com o banco de dados;
- Domain: Camada onde estão configuradas as Entidades e os contratos;

### Tecnologias utilizadas:
<div style="display: inline_block">
  <img align="center" alt=".NET" src="https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white" />
  <img align="center" alt="SQLite" src="https://img.shields.io/badge/SQLite-07405E?style=for-the-badge&logo=sqlite&logoColor=white"/>
</div>
