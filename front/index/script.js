const url = 'http://localhost:4877/'

fetch(`${url}Produto/buscarTodos`)
  .then((response) => response.json())
  .then(json => {
    json.forEach(product => {
      var newDiv = document.createElement('div');
      newDiv.className = 'item-promo';

      newDiv.innerHTML = `
        <div class="cart">
          <button class="overlap-group" onclick="adicionarCarrinho(${product.id})">
            <div class="div">
              <img class="img" src="img/cart1-9.svg" />
              <div class="text-wrapper">Adicionar ao carrinho</div>
            </div>
          </button>
           <div class="img-wrapper"><img class="combo-galera" src="${product.imagem}" /></div>
        </div>
        <div class="frame">
          <div class="text-wrapper-2">${product.nomeProduto}</div>
          <div class="div-wrapper">
            <div class="text-wrapper-3">${product.preco} R$</div>
          </div>
        </div>
      `;

      document.getElementById('container').appendChild(newDiv);
    });
  })
  .catch(error => console.error('Error fetching data:', error));

async function adicionarCarrinho(id) {
  const response = await fetch(`${url}Carrinho/adicionar?idProduto=${id}&quantidade=1`,
    { method: 'POST', })

  if (response.ok) { alert('Item adicionado no carrinho.') }
  else { alert('Erro ao adicionar no carrinho.') }
}

//   function adicionarCarrinho2() {
//     // sessionStorage.setItem('produtox',
//     //   {
//     //     'id': 1,
//     //     'nomeProduto': 'rapaduris'
//     //   })

//     document.cookie = `produto= ${JSON.stringify({
//       'id': 6,
//       'nomeProduto': 'carai'
//     })}`
// alert('adicionado')
//   }