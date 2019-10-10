// Call the dataTables jQuery plugin
$(document).ready(function() {
    $('#dataTable').DataTable({
        language: {
            "sEmptyTable": "Nenhum registro encontrado",
            "sInfo": "Mostrando _START_ ate _END_ de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando 0 ate 0 de 0 registros",
            "sInfoFiltered": "(Filtrados de _MAX_ registros)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "_MENU_ resultados por pagina",
            "sLoadingRecords": "Carregando...",
            "sProcessing": "Carregando...",
            "sZeroRecords": "Nenhum registro encontrado",
            "sSearch": "Pesquisar Vagas:",
            "oPaginate": {
                "sNext": "Proximo",
                "sPrevious": "Anterior",
                "sFirst": "Primeiro",
                "sLast": "Ultimo"
            },
            "oAria": {
                "sSortAscending": ": Ordenar colunas de forma ascendente",
                "sSortDescending": ": Ordenar colunas de forma descendente"
            }
        }
    });
});
