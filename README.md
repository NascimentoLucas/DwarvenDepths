# 🎯 DwarvenDepths 
O objetivo do jogo é acumular o maior número de pontos possível. Para isso, você deve usar seus minions em diferentes níveis, onde eles podem gerar recursos ou utilizá-los para criar novos itens.

# 🛠️ Análise Técnica
## Responsividade
Nesse projeto de tycon primeiro foquei em validar as duas mecanicas fundamentais para a experiencia do usuário: a criação dos níveis em ordem e o scroll deles pela tela, fiz isso em uma branch separada a fim de não poluir o projeto. Precisei validar essas mecânicas antes pois são importantes para a responsividade do jogo.
## Scriptable Objects (SO)
Vários aspectos do balanceamento do jogo envolvem ajustes de valores, como bônus dos minions e custos dos recursos. Por isso, optei por utilizar Scriptable Objects (SOs) sempre que possível, visando otimizar a execução em tempo de execução (reduzindo o consumo de memória) e facilitar o desenvolvimento, permitindo ajustes e testes de forma prática.
## Ferramentas
Implementei uma janela no editor para visualizar todos os itens criados no projeto, tornando mais fácil verificar as propriedades (como custo, componentes, etc.) de cada item. Além disso, criei testes no Unity para validar as relações e componentes dos itens.
## Experiencia do usuário 
Foquei em proporcionar uma melhor experiência ao usuário com a UI. Para isso, implementei recursos como a contagem de minions e a ordenação automática da lista de recursos conforme o objetivo.

# 📦 Assets
* [Pixel Prototype Player Sprites](https://assetstore.unity.com/packages/2d/characters/pixel-prototype-player-sprites-221542)
* [Pixel Caves](https://assetstore.unity.com/packages/2d/environments/pixel-caves-136235)
* [Cartoon Ground and Floor Textures](https://assetstore.unity.com/packages/2d/textures-materials/floors/cartoon-ground-and-floor-textures-68398)
