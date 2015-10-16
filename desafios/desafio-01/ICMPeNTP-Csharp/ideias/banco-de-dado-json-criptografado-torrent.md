# A Motivação

Disponibilizar dados descentralizados e criptografados.

# O Problema

Utilizar bancos de dados de terceiros e de dados fechados pode não ser uma boa escolha par nossos fins, precisamos de uma garantia que se quisermos os dados não possam ser acessados por agencias governamentais e outros.

# A Solução

Criar um banco de dados simples baseado em JSON criptografado fortemente, onde apenas quem possui uma chave privada possa descriptografar, e a cada acesso adicionar esse OPLOG em um tipo de BLOCKCHAIN como o bitcoin usa para guardar todas suas transções, porém TAMBÉM de forma criptografada.

## O Projeto

- Blockchain: de acesso aos dados, toda vez que uma chave privada descriptografar deverá ser gravado no blockchain, depois do callback de sucesso que a informação é entregue para ser descriptografada localmente;
- JSON criptografado: criptografar o JSON de forma irreversível, onde as busca pelos seus valores darão pela criptografia da busca para que nenhum dado seja revertido ao seu valor original.
- Aplicativo de buscas no banco: utiliazará sua chave privada, o blockchain para buscar os dados, depois de recebê-los pode cachear localmente e as buscas são feitas criptografando seus dados e comparando com os dados entregues. 