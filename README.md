# PortalDataTask
 
Projeto backend para demonstrar a lógica de cadastro de atividades, sendo desenvolvido com arquitetura em camadas, webapi com enpoints de CRUD. 
Ao receber a chamada para Cadastro, é enviado para fila do rabbit. Neste backend tem um projeto de consumer as m3nsagens da fila para realizar o cadastro. 
Há testes Unitários e teste integrado 
