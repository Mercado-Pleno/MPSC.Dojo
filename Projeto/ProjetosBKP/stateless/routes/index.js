var express = require('express');
var router = express.Router();

/* Middleware */

function verificarAutenticacao(req, res, next) {
  // Paths que precisam de autorizacao:
  if (req.path == '/estilos') {
    var authOk = false;
    var auth = req.headers['authorization'];
    console.log(">>> Header: " + auth);
    if (auth) {
      // Pula o "BASIC ";
      var b64 = new Buffer(auth.substring(6), 'base64');
      var sobj = b64.toString();
      var objeto = JSON.parse(sobj);         
      console.log(">>> Conteudo: " + JSON.stringify(objeto));
      if (objeto.username == "fulano"
          && objeto.senha == "teste") {
          console.log(">>> OK");
          authOk = true;
      }
    }
    if (!authOk) {
      console.log(">>> Falha!");
      res.json(401,"erro");
    }
    else {
      return next();
    }
  }
  else {
    return next();
  }
}

/* Todos os requests devem ser autenticados */
router.use(verificarAutenticacao);

/* Login */
router.get('/login', function(req,res) {
  res.render('login', { title: 'LOGIN' });
});
router.post('/login',function(req, res) {
  var username = req.body.username; 
  var senha = req.body.senha;
  console.log("Username: " + username + " senha: " + senha);
  if (username == "fulano" && senha == "teste") {
    res.json(200,"ok");
  }
  else {
    res.json(403,"Erro");
  }
});

/* GET home page. */
router.get('/', function(req, res) {
  res.render('index', { title: 'Estilos de artistas' });
});

/* Lista de artistas */
router.get('/estilos', function(req, res) {
  Estilo.find({})
  .exec(function(erro,estilos) {
    if(!erro) {
      res.header('Content-type', 'application/json; charset=utf-8');
      res.json(200,estilos);
    }
    else {
      throw new Error('Erro ao acessar banco: ' + erro);
    }
  });
});

module.exports = router;
