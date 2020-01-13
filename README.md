## Informações Importantes

**O aplicativo ainda não conecta com o Azure** 

* Todos os dados são salvos localmente no SQL Server.
* Todas as imagens são localmente salvas na pasta StatueStoreWebApplic/dpProdutoImagens/ do aplicativo web 
(em [statue-store-web](https://github.com/EspetacularGus/statue-store-web))

## Manual do Usuário

Primeiramente temos a tela de login, nessa tela o funcionário deverá realizar o login com seu CPF e senha padrão fornecida pelo sistema, após o primeiro login o usuário poderá realizar a troca da senha em uma janela que aparecerá logo em seguida.

![loging C](https://user-images.githubusercontent.com/59635709/72236341-3901e680-35b5-11ea-81ce-33a389a5f0cf.png)

Após entrar, o funcionário irá se deparar com um menu com uma mensagem de boas vindas e seus dados, inclusive seu nível de acesso.
Ao lado esquerdo da tela é possível encontrar um menu lateral, que pode ser expandido e escondido clicando no ícone das três linhas no canto superior esquerda, nesse menu se encontram as opções de cadastro e visualização de dados.

![menu C](https://user-images.githubusercontent.com/59635709/72236340-38695000-35b5-11ea-8d0c-2032ce7e8b2e.png)

![menu lateral C](https://user-images.githubusercontent.com/59635709/72236339-38695000-35b5-11ea-8967-7e56098a9210.png)

É importante lembrar que nem todas as opções estarão disponíveis para todos os usuários, a disponibilidade das mesmas depende do nível de acesso do funcionário logado, nesse caso, todas elas estão disponíveis pois o usuário teste é de nível administrador.

![cadastro func C](https://user-images.githubusercontent.com/59635709/72236337-38695000-35b5-11ea-82e4-b6048be53fd3.png)

Acima temos um exemplo de uma tela de cadastro, no caso apresentado é um formulário de cadastro de outros funcionários, porém formulários semelhantes estão disponíveis também para o cadastro de produtos, fornecedores, grupos e subgrupos de produtos.

![tela pesquisa](https://user-images.githubusercontent.com/59635709/72236336-38695000-35b5-11ea-81a9-e22aaa2c7b51.png)

Já na função de visualização de dados, o usuário pode escolher entre várias categorias e, se desejável, filtrar os dados escrevendo o que deseja encontrar, o exemplo acima mostra a visualização de todos os grupos de produtos presentes no sistema, sem o uso de quaisquer filtros.

As outras duas opções restantes dizem respeito a visualização de estoque e de pedidos, no caso de falta de estoque em qualquer um dos produtos cadastrados, um email será enviado ao funcionário perguntando se o mesmo deseja realizar a reposição do estoque do produto.

![estoque C](https://user-images.githubusercontent.com/59635709/72236335-38695000-35b5-11ea-859f-a9c0d67ce1d9.png)

Acima a tela de visualização de estoque e abaixo a de pedidos, ambas permitem a busca por meio do ID do cadastro.

![pedidos C](https://user-images.githubusercontent.com/59635709/72236334-37d0b980-35b5-11ea-893d-a5081ca22654.png)
