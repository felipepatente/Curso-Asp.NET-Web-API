var tbody = document.querySelector('table tbody');
		var aluno = {};

		function Cadastrar() {
			
			aluno.Nome = document.querySelector('#nome').value;
			aluno.Sobrenome = document.querySelector('#sobrenome').value;
			aluno.Telefone = document.querySelector('#telefone').value;
			aluno.Ra = document.querySelector('#ra').value;

			console.log(aluno);

			if(aluno.Id === undefined || aluno.Id === 0){
				SalvarEstudantes('POST', 0, aluno);
			}else{
				SalvarEstudantes('PUT', aluno.Id, aluno);
			}

			CarregaEstudantes();
		}

		function Cancelar() {
			
			var btnSalvar = document.querySelector('#btnSalvar');
			var btnCancelar = document.querySelector('#btnCancelar');
			var titulo = document.querySelector('#titulo');

			document.querySelector('#nome').value = '';
			document.querySelector('#sobrenome').value = '';
			document.querySelector('#telefone').value = '';
			document.querySelector('#ra').value = '';

			aluno = {};

			btnSalvar.textContent = 'Cadastrar';
			btnCancelar.textContent = 'Limpar';

			titulo.textContent = 'Cadastrar Aluno';

		}

		function CarregaEstudantes() {

			tbody.innerHTML = '';

			var xhr = new XMLHttpRequest();

			xhr.open(`GET`, `https://localhost:44309/api/Aluno/`, true);
			
			xhr.onload = function () {
				
				var estudantes = JSON.parse(this.responseText);

				for (var indice in estudantes) {
					adicionaLinha(estudantes[indice]);
				}				
			}
			
			xhr.send();				
		}

		function SalvarEstudantes(metodo, id, corpo) {

			var xhr = new XMLHttpRequest();

			if(id === undefined || id === 0){
				id = '';
			}

			xhr.open(metodo, `https://localhost:44309/api/Aluno/${id}`, false);
			
			xhr.setRequestHeader('content-type','application/json');	
			xhr.send(JSON.stringify(corpo));											
		}

		CarregaEstudantes();

		function editarEstudante(estudante){
			
			var btnSalvar = document.querySelector('#btnSalvar');
			var btnCancelar = document.querySelector('#btnCancelar');
			var titulo = document.querySelector('#titulo');

			document.querySelector('#nome').value = estudante.Nome;
			document.querySelector('#sobrenome').value = estudante.Sobrenome;
			document.querySelector('#telefone').value = estudante.Telefone;
			document.querySelector('#ra').value = estudante.Ra;

			btnSalvar.textContent = 'Salvar';
			btnCancelar.textContent = 'Cancelar';

			titulo.textContent = `Editar Aluno ${estudante.Nome}`;

			aluno = estudante;

			console.log(aluno);
		}

		function excluirEstudante(id){

			var xhr = new XMLHttpRequest();
			xhr.open(`DELETE`, `https://localhost:44309/api/Aluno/${id}`, false);
			xhr.send();			
		}

		function excluir(id) {
			
			excluirEstudante(id)
			CarregaEstudantes();
		}

		function adicionaLinha(estudante) {
			
			var trow = `<tr>
							<td>${estudante.Nome}</td>
							<td>${estudante.Sobrenome}</td>
							<td>${estudante.Telefone}</td>
							<td>${estudante.Ra}</td>
							<td>
								<button class="btn btn-info" onclick='editarEstudante(${JSON.stringify(estudante)})'>Editar</button>
								<button class="btn btn-danger"onclick='excluir(${estudante.Id})'>Deletar</button>
							</td>
						</tr>
					   `

			tbody.innerHTML += trow;
		}